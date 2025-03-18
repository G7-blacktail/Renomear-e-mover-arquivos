using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Diretórios de origem e destino
        string sourceDirectory = @"\\dc\AC NACIONAL\Correspondentes\0 - DESLIGAMENTOS\DOCUMENTOS\ENTREVISTA DESLIGAMENTO";
        // string destinationBaseDirectory=@"\\dc\Dossies AGRs\Inativos";
        string destinationBaseDirectory = @"\\dc\Dossies AGRs\Ativos";

        // Obter todos os arquivos PDF no diretório de origem
        var pdfFiles = Directory.GetFiles(sourceDirectory, "*.pdf");

        // Obter todas as subpastas no diretório de destino
        var subfolders = Directory.GetDirectories(destinationBaseDirectory);

        // Listas para armazenar resultados
        List<string> movedFiles = new List<string>();
        List<string> notFoundFiles = new List<string>();

foreach (var pdfFile in pdfFiles)
{
    try
    {
        // Extrair o nome do arquivo sem extensão
        string fileName = Path.GetFileNameWithoutExtension(pdfFile);
        
        // Procurar o nome do candidato após o último "-"
        string candidateName = ExtractCandidateNameFromFileName(fileName);

        if (!string.IsNullOrEmpty(candidateName))
        {
            // Procurar a subpasta mais parecida com o nome do candidato
            string closestMatch = FindClosestFolderMatch(candidateName, subfolders);

            if (!string.IsNullOrEmpty(closestMatch))
            {
                // Caminho completo do destino
                string destinationPath = Path.Combine(closestMatch, Path.GetFileName(pdfFile));

                // Mover o arquivo
                File.Move(pdfFile, destinationPath);

                movedFiles.Add($"{Path.GetFileName(pdfFile)} movido para {closestMatch}");
            }
            else
            {
                notFoundFiles.Add($"{Path.GetFileName(pdfFile)} não encontrado: não há pasta com o nome {candidateName}");
            }
        }
        else
        {
            notFoundFiles.Add($"{Path.GetFileName(pdfFile)} não encontrado: não foi possível extrair o nome do candidato");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao mover o arquivo {Path.GetFileName(pdfFile)}: {ex.Message}");
    }
}

// Exibir resultados
Console.WriteLine("\nArquivos movidos:");
movedFiles.ForEach(Console.WriteLine);

Console.WriteLine("\nArquivos não encontrados:");
notFoundFiles.ForEach(Console.WriteLine);

static string ExtractCandidateNameFromFileName(string fileName)
{
    // Presume que o nome do candidato é após o último "-"
    int lastDashIndex = fileName.LastIndexOf(' ENTREVISTA');
    if (lastDashIndex >= 0 && lastDashIndex < fileName.Length - 1)
    {
        return fileName.Substring(0, lastDashIndex).Trim();
    }
    return null;
}

static string FindClosestFolderMatch(string candidateName, string[] subfolders)
{
    // Procurar a subpasta que contenha o nome do candidato (ignorando espaços e case)
    foreach (var folder in subfolders)
    {
        string folderName = Path.GetFileName(folder).Trim().ToLower();
        string candidateNameLower = candidateName.Trim().ToLower();

        if (folderName == candidateNameLower)
        {
            return folder;
        }
    }

    return null;
}
    }
}
