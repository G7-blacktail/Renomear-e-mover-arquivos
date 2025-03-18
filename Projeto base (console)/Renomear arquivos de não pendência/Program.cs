// using System;
// using System.IO;
// using System.Text;
// using System.Text.RegularExpressions;
// using iText.Kernel.Pdf;
// using iText.Kernel.Pdf.Canvas.Parser;
// using iText.Kernel.Pdf.Canvas.Parser.Listener;

// class Program
// {
//     static void Main(string[] args)
//     {
//         // Caminho do diretório onde os PDFs estão localizados
//         string sourceDirectory = @"C:\Users\Willian\Desktop\DOCUMENTOS\COMUNICACAO RESCISAO CONTRATUAL";

//         // Obtém todos os arquivos PDF no diretório de origem
//         var pdfFiles = Directory.GetFiles(sourceDirectory, "*.pdf");

//  foreach (var pdfFile in pdfFiles)
// {
//     try
//     {
//         string candidateName = ExtractCandidateNameFromPdf(pdfFile);

//         if (!string.IsNullOrEmpty(candidateName))
//         {

//             string newFileName = $"{candidateName} DECLARAÇÃO DE ARQUIVAMENTO DE DOSSIE.pdf";
//             string newFilePath = Path.Combine(sourceDirectory, newFileName);

//             // Verifica se o arquivo já existe para evitar sobreposição
//             if (File.Exists(newFilePath))
//             {
//                 Console.WriteLine($"Arquivo {newFileName} já existe. Não será renomeado.");
//                 continue;
//             }

//             // Renomeia o arquivo
//             File.Move(pdfFile, newFilePath);

//             Console.WriteLine($"Arquivo {Path.GetFileName(pdfFile)} renomeado para {newFileName}");
//         }
//         else
//         {
//             Console.WriteLine($"Nome do candidato(a) não encontrado em {Path.GetFileName(pdfFile)}");
//         }
//     }
//     catch (Exception ex)
//     {
//         Console.WriteLine($"Erro ao renomear o arquivo {Path.GetFileName(pdfFile)}: {ex.Message}");
//     }
// }
//     }

// static string ExtractCandidateNameFromPdf(string pdfFilePath)
// {
//     using (PdfReader pdfReader = new PdfReader(pdfFilePath))
//     using (PdfDocument pdfDoc = new PdfDocument(pdfReader))
//     {
//         // Extração do texto do PDF
//         for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
//         {
//             ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
//             string text = PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(i), strategy);

//             // Localizar o nome do destinatário(a) no texto do PDF
//             string searchPattern = " Destinatario ";
//             int startIndex = text.IndexOf(searchPattern);
//             if (startIndex >= 0)
//             {
//                 startIndex += searchPattern.Length;

//                 // Encontrar o fim do nome do candidato(a), que termina antes de "por conformação"
//                 int endIndex = text.IndexOf(", CPF", startIndex);
//                 if (endIndex == -1)
//                 {
//                     // Se não encontrar "CPF:", verificar se o nome ocupa duas linhas
//                     int nextLineIndex = text.IndexOf(Environment.NewLine, startIndex);
//                     if (nextLineIndex != -1)
//                     {
//                         string nextLine = text.Substring(nextLineIndex + Environment.NewLine.Length);
//                         endIndex = nextLine.IndexOf(", CPF");
//                         if (endIndex != -1)
//                         {
//                             endIndex = nextLineIndex + endIndex;
//                         }
//                         else
//                         {
//                             endIndex = text.Length;
//                         }
//                     }
//                     else
//                     {
//                         endIndex = text.Length;
//                     }
//                 }

//                 string candidateName = text.Substring(startIndex, endIndex - startIndex).Trim();
//                 // Remover "AGR " do início do nome
//                 candidateName = candidateName.Replace("AGR ", "");
//                 return candidateName;
//             }
//         }
//     }

//     return null;
// }


// string SanitizeFileName(string fileName)
// {
//     return fileName.Replace(" ", "_").Replace(":", "_").Replace(";", "_").Replace("/", "_").Replace("\\", "_").Replace("?", "_").Replace("*", "_").Replace("<", "_").Replace(">", "_").Replace("|", "_").Replace("\"", "_").Replace("'", "_");
// }
// }
// using System;
// using System.IO;
// using System.Text;
// using System.Text.RegularExpressions;
// using iText.Kernel.Pdf;
// using iText.Kernel.Pdf.Canvas.Parser;
// using iText.Kernel.Pdf.Canvas.Parser.Listener;

// class Program
// {
//     static void Main(string[] args)
//     {
//         // Caminho do diretório onde os PDFs estão localizados
//         string sourceDirectory = @"\\dc\F\Lidersis\ARQUIVO ATIVO\Auditorias\JVS\Declaração de arquivamento digital";

//         // Obtém todos os arquivos PDF no diretório de origem
//         var pdfFiles = Directory.GetFiles(sourceDirectory, "*.pdf");

//         foreach (var pdfFile in pdfFiles)
//         {
//             try
//             {
//                 string candidateName = ExtractCandidateNameFromPdf(pdfFile);

//                 if (!string.IsNullOrEmpty(candidateName))
//                 {
//                     string newFileName = $"{SanitizeFileName(candidateName)} - DECLARAÇÃO DE ARQUIVAMENTO DE DOSSIE.pdf";
//                     string newFilePath = Path.Combine(sourceDirectory, newFileName);

//                     // Verifica se o arquivo já existe para evitar sobreposição
//                     if (File.Exists(newFilePath))
//                     {
//                         Console.WriteLine($"Arquivo {newFileName} já existe. Não será renomeado.");
//                         continue;
//                     }

//                     newFilePath = SanitizeFileName(newFilePath);

//                     // Renomeia o arquivo
//                     File.Move(pdfFile, newFilePath);

//                     Console.WriteLine($"Arquivo {Path.GetFileName(pdfFile)} renomeado para {newFileName}");
//                 }
//                 else
//                 {
//                     Console.WriteLine($"Nome do candidato(a) não encontrado em {Path.GetFileName(pdfFile)}");
//                 }
//             }
//             catch (Exception ex)
//             {
//                 Console.WriteLine($"Erro ao renomear o arquivo {Path.GetFileName(pdfFile)}: {ex.Message}");
//             }
//         }
//     }

//     static string ExtractCandidateNameFromPdf(string pdfFilePath)
//     {
//         using (PdfReader pdfReader = new PdfReader(pdfFilePath))
//         using (PdfDocument pdfDoc = new PdfDocument(pdfReader))
//         {
//             // Extração do texto do PDF
//             for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
//             {
//                 ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
//                 string text = PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(i), strategy);

//                 // Localizar o nome do candidato(a) no texto do PDF
//                 string searchPattern = " (AGR)";
//                 int startIndex = text.IndexOf(searchPattern);
//                 if (startIndex >= 0)
//                 {
//                     startIndex += searchPattern.Length;

//                     // Encontrar o fim do nome do candidato(a), que termina antes de "por conformação"
//                     int endIndex = text.IndexOf(",", startIndex);
//                     if (endIndex == -1)
//                     {
//                         // Se não encontrar "CPF:", verificar se o nome ocupa duas linhas
//                         int nextLineIndex = text.IndexOf(Environment.NewLine, startIndex);
//                         if (nextLineIndex != -1)
//                         {
//                             string nextLine = text.Substring(nextLineIndex + Environment.NewLine.Length);
//                             endIndex = nextLine.IndexOf("CPF:");
//                             if (endIndex != -1)
//                             {
//                                 endIndex = nextLineIndex + endIndex;
//                             }
//                             else
//                             {
//                                 endIndex = text.Length;
//                             }
//                         }
//                         else
//                         {
//                             endIndex = text.Length;
//                         }
//                     }

//                     string candidateName = text.Substring(startIndex, endIndex - startIndex).Trim();
//                     // Remover "AGR " do início do nome
//                     candidateName = candidateName.Replace("AGR ", "");
//                     return candidateName;
//                 }
//             }
//         }

//         return null;
//     }

//     static string SanitizeFileName(string fileName)
//     {
//         return fileName.Replace(" ", "_").Replace(":", "_").Replace(";", "_").Replace("/", "_").Replace("\\", "_").Replace("?", "_").Replace("*", "_").Replace("<", "_").Replace(">", "_").Replace("|", "_").Replace("\"", "_").Replace("'", "_");
//     }
// }


/* 
* Necessário particionar cada código para uma classe e fazer o programa identificar qual o tipo de arquivo está sendo lido
*/

using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;

class Program
{
    static void Main(string[] args)
    {
        // Caminho do diretório onde os PDFs estão localizados
        string sourceDirectory = @"Z:\AC NACIONAL\Correspondentes\0 - MALA DIRETA\Declaracao_Residencia";

        // Obtém todos os arquivos PDF no diretório de origem
        var pdfFiles = Directory.GetFiles(sourceDirectory, "*.pdf");

        foreach (var pdfFile in pdfFiles)
        {
            try
            {
                // Ler o conteúdo do PDF para extrair o nome do candidato(a)
                string candidateName = ExtractCandidateNameFromPdf(pdfFile);

                if (!string.IsNullOrEmpty(candidateName))
                {
                    // Sanitize the candidate's name to remove invalid characters
                    candidateName = SanitizeFileName(candidateName);

                    // Novo nome do arquivo com a sigla REA no início
                    string newFileName = $"{candidateName} - DECLARACAO RESIDENCIA.pdf";
                    string newFilePath = Path.Combine(sourceDirectory, newFileName);

                    // Verifica se o arquivo já existe para evitar sobreposição
                    if (File.Exists(newFilePath))
                    {
                        Console.WriteLine($"Arquivo {newFileName} já existe. Não será renomeado.");
                        continue;
                    }

                    // Log para verificar nomes completos
                    Console.WriteLine($"Tentando renomear de:\n{pdfFile}\nPara:\n{newFilePath}");

                    // Renomeia o arquivo
                    File.Move(pdfFile, newFilePath);

                    Console.WriteLine($"Arquivo {Path.GetFileName(pdfFile)} renomeado para {newFileName}");
                }
                else
                {
                    Console.WriteLine($"Nome do candidato(a) não encontrado em {Path.GetFileName(pdfFile)}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao renomear o arquivo {Path.GetFileName(pdfFile)}: {ex.Message}");
            }
        }
    }

static string ExtractCandidateNameFromPdf(string pdfFilePath)
{
    using (PdfReader pdfReader = new PdfReader(pdfFilePath))
    using (PdfDocument pdfDoc = new PdfDocument(pdfReader))
    {
        // Extração do texto do PDF
        for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
        {
            ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
            string text = PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(i), strategy);

            // Localizar o nome do candidato(a) no texto do PDF
            string searchPattern = "Eu, ";
            int startIndex = text.IndexOf(searchPattern);
            if (startIndex >= 0)
            {
                startIndex += searchPattern.Length;

                // Encontrar o fim do nome do candidato(a), que termina antes de "CPF"
                int endIndex = text.IndexOf(", CPF", startIndex);
                if (endIndex == -1)
                {
                    // Continuar lendo a próxima linha até encontrar o fim do nome
                    string nextLine = text.Substring(startIndex);
                    while (true)
                    {
                        int nextLineIndex = nextLine.IndexOf(Environment.NewLine);
                        if (nextLineIndex != -1)
                        {
                            nextLine = nextLine.Substring(nextLineIndex + Environment.NewLine.Length);
                            endIndex = nextLine.IndexOf("CPF");
                            if (endIndex != -1)
                            {
                                endIndex = startIndex + nextLineIndex + endIndex;
                                break;
                            }
                        }
                        else
                        {
                            endIndex = text.Length;
                            break;
                        }
                    }
                }

                string candidateName = text.Substring(startIndex, endIndex - startIndex).Trim();
                candidateName = SanitizeFileName(candidateName); // Remover caracteres impróprios do nome do candidato
                candidateName = System.Text.RegularExpressions.Regex.Replace(candidateName, @"_(?=\w)", "");
                return candidateName;
            }
        }
    }

    return null;
}


static string SanitizeFileName(string fileName)
{
    return Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(), "_"));
}
}

