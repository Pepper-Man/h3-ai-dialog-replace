using System;
using System.IO;
using Bungie;
using Bungie.Tags;
using System.Text.RegularExpressions;

namespace AIDialogueUpdater
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // MB Initialisation
            string h3ek = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\H3EK";
            ManagedBlamSystem.InitializeProject(InitializationType.TagsOnly, h3ek);

            while (true)
            {
                // Get tag path
                Console.WriteLine("Enter tags-relative path to .ai_mission_dialogue tag, without extension: ");
                string tagPathString = NormalisePath(Console.ReadLine());
                TagPath tagPath = TagPath.FromPathAndExtension(tagPathString, "ai_mission_dialogue");

                try
                {
                    using (TagFile tagFile = new TagFile(tagPath))
                    {
                        // Get original part of string to replace
                        Console.WriteLine("Enter section of path to be replaced: ");
                        string toReplace = NormalisePath(Console.ReadLine());

                        // Get replacement part
                        Console.WriteLine("Enter replacement part for new path: ");
                        string replaceWith = NormalisePath(Console.ReadLine());

                        int linesCount = ((TagFieldBlock)tagFile.SelectField("Block:lines")).Elements.Count;
                        Console.WriteLine($"Dialogue tag has {linesCount} lines!");

                        for (int i = 0; i < linesCount; i++)
                        {
                            string name = ((TagFieldElementStringID)tagFile.SelectField($"Block:lines[{i}]/StringID:name")).Data.ToString().Trim();
                            int variantsCount = ((TagFieldBlock)tagFile.SelectField($"Block:lines[{i}]/Block:variants")).Elements.Count;

                            for (int j = 0; j < variantsCount; j++)
                            {
                                TagPath soundReference = ((TagFieldReference)tagFile.SelectField($"Block:lines[{i}]/Block:variants[{j}]/Reference:sound")).Path;
                                string updatedPath = soundReference.RelativePath.Replace(toReplace, replaceWith);
                                TagPath newSoundReference = TagPath.FromPathAndExtension(updatedPath, "sound");
                                ((TagFieldReference)tagFile.SelectField($"Block:lines[{i}]/Block:variants[{j}]/Reference:sound")).Path = newSoundReference;
                            }

                            Console.WriteLine("\"" + name + "\"...done!");
                        }
                        try
                        {
                            tagFile.Save();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"TAG SAVE ERROR - Failed to save tag properly. Please try closing the EK tools, then re-run this program and try again.\nException: {ex}\n");
                            Console.WriteLine("Press enter to exit...");
                            Console.ReadLine();
                        }

                        Console.WriteLine("\nTagfile successfully edited and saved! Press enter to exit...");
                        Console.ReadLine();
                        return;
                    }
                }
                catch (TagLoadException ex)
                {
                    Console.WriteLine($"\nTAG LOAD EXCEPTION - Perhaps you entered an invalid tag path?\nException: {ex}\n");
                    Console.WriteLine("Please try again.");
                }
            }

            
        }

        static string NormalisePath(string input)
        {
            string unified = input.Replace('/', '\\');
            return Regex.Replace(unified, @"\\{2,}", @"\");
        }
    }
}
