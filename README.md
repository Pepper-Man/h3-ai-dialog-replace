# AI Mission Dialogue Path Updater
Small C# ManagedBlam tool for quickly bulk updating filepaths in a `.ai_mission_dialogue` tag.
Replaces an existing substring of the filepath with another.
E.g:  
"sound\h2_sound\sound\dialog\levels\03_earthcity\mission\l03_8070_grf"  
|  
V  
"sound\h2_03a\dialog\levels\03_earthcity\mission\l03_8070_grf"

# Requirements
* Requires [.NET 4.8 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48)

# Usage
* Place `AIDialogueUpdater.exe` into a folder alongside `ManagedBlam.dll` (can be found in `H3EK\bin`)
* Run the program
* Enter the tags-relative path to the `.ai_mission_dialogue` tag, e.g. `halo_2\levels\singleplayer\oldmombasa\oldmombasa_dialog`
* Enter the substring to be replaced, e.g. `sound\h2_sound\sound`
* Enter the string to replace with, e.g. `sound\h2_03a`
* The program will do its thing, hit enter to close when done