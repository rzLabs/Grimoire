# Foreword

The documentation herein and linked to by this article is created and maintained by `iSmokeDrow` (`@gmail.com`). It is up-to-date to the best of the authors knowledge and so any bugs/issues found should be reported via github issues or via email.

# About

`Grimoire` is a multi-purpose `Rappelz` editing/administrating suite of tools. *(such as an `SPR Generator` or `Dump Merger`)*

# First Use

**STOP!** The very first thing you need to do is to `configure` the software. This can be done two ways:

* Open `Grimoire` and click the `Settings` button. 
* Configure as needed and close the menu *(settings are automatically written to disk)*

or

* Open the directory `Grimoire` is locafted in and open the `Config.json` with your favorite editor.
* Configured as needed and save the file *(If `Grimoire` is open you will need to restart it)*

> **Note:** Directories can be `relative` or `absolute`

> **Note:** You can find a table of possible configuration settings and their acceptable values below

## Configuration Settings

| Name | Category | Description | Value-Type |
|-|-|-|-|
| BuildDirectory | Grim | Directory where files created by Grimoire will be written to | string |
| DumpDirectory | Grim | Directory where utilities like SPR Generator and Dump Updater can find a premade `/dump/` | string |
| Codepage | Grim | Encoding used in some functionality | int |
| Styles | Tab | Styles to be listed in the New: tab list | string[] |
| DefaultStyle | Tab | Style to be loaded on-start (if any, elsewise use "NONE") | string |
| Engine | DB | Type of database engine to be used (0 = MsSQL, 1 = MySQL) | int |
| IP | DB | Target machine to connect to for SQL operations. | string |
| Port | DB | Target machine port to connections related to SQL operations. | int |
| Trusted | DB | If true Grimoire will attempt to make trusted connections on the SA account for MsSQL connections. Note: this will only work when your SQL instance is installed as a default instance with Mixed Mode Authentication enabled on your LOCAL network. | bool |
| WorldName | DB | Name of the Arcadia database. | string |
| WorldUser | DB | Username for the Arcadia database. (Leave blank if using Trusted) | string |
| WorldPass | DB | Password for the user of the Arcadia database. (Leave blank if using Trusted) | string |
| Backup | DB | If true, tables will be backed up via scripting before changes are made to them | bool |
| DropOnExport | DB | If true, tables will be dropped via the drop statement during save operations. | bool |
| Timeout | DB | The amount of time that can elapse before SQL operations will timeout. | int |
| Struct_AutoLoad | RDB | Not implemented! | bool |
| Directory | RDB | Directory where structures can be located. (/structures by default) | bool |
| Encoding | RDB | Encoding to be used in rdb related operations. Note: this must match a name on the list of encoding for RDB tabs. | string |
| LoadDirectory | RDB | Default directory to be used in .rdb load/save operations. | string |
| AppendASCII | RDB | If true, (ascii) will be appended to the names of files being written to disk. | bool |
| SaveHashed | RDB | If true, file names will be converted/hashed during save operations. | bool |
| CSV_Directory | RDB | Directory where .csv will be created. | string |
| SQL_Directory | RDB | Not implemented. | string |
| LoadDirectory | Data | Default directory be used in data.000 load/save operations. | string |
| Encoding | Data | Encoding used in data.xxx operations | int |
| Backup | Data | If true, data.xxx files will be backed up before being changed by Grimoire | bool |
| UseModifiedXOR | Data | If true, DataCore will use the provided ModifiedXORKey in operations | bool |
| ModifiedXORKey | Data | Key to be used in DataCore operations if UseModifiedXOR is set to true | int[] (array) |
| ClearOnCreate | Data | If true, newly created data.000 will not be loaded and displayed | bool |
| AutoClear | Hash | If true, the hash grid will be cleared after a conversion occurs. | bool |
| AutoConvert | Hash | If true, files added to the hash grid will be automatically hashed. | bool |
| Type | Hash | Do not change this! | int |
| Directory | Log | Directory where new log files will be written | string |
| DisplayLevel | Log | Not Implemented! | int |
| WriteLevel | Log | Not Implemented! | int |
| RefreshInterval | Log | Time (in seconds) before the log manager will refresh the displayed logged entries. | int |
| SaveOnExit | Log | If true, logs will be written to disk during the exit operation. | bool |
| Directory | Flag | Directory where flag list .txt are loaded from | string |
| Default | Flag | Name (without extension) of the default flag list .txt | string |
| ClearOnChange | Flag | If true, when flag lists are changed all selected flags will be unselected. | bool |
| OverwriteExisting | DumpUpdater | If true, files in the target dump directory will be automatically ovewritten during an update operation | bool |
| IgnoreSize | SPR | If true, the spr generator will not warn/delete of icons with a size different than expected | bool |
| ShowWarnings | SPR | If true, the spr generator will show warnings about icons that could not be validated and have been removed | bool |
| ShowIgnored | SPR | If true, the spr generator will show a list of all ignored (unvalidated) icon files after a generate operation | bool |
| Directory | Localization | Directory where localization .xml are located | string |
| Locale | Localization | Name (without extension) of the target locale .xml | string |

## Using Data Utility

*Most of the instructions below assume you have an open and properly configured instance of Grimoire*

### Opening the Rappelz file index (data.000)

The data.000 can be opened in a couple ways:

1. Click the Load button
2. In Windows Explorer locate the desired data.000 and drag it on-top of the data tab.
3. Press the `CTRL + O` key combination

### Creating new data.xxx

*(Please note new data files will be created in the output folder, if backups is toggled than any existing data.xxx in this directory will be preserved, otherwise they will be overwritten)*

1. Open a new `Data` tab
2. Click `New`or press the `CTRL + N` key combination
3. Select the folder containing a `dump` 
	1. The folder should contain files categorized by the extension
	2. e.g. `cob/dds/jpg` etc
	3. Any files out of place will cause `File Not Found` exceptions. *(e.g. a `.nfe` file located in the /nfm/ directory!)*
4. Wait til complete!

### Rebuild / Shrink client data.xxx

1. Open new `Data` tab
2. Open a data.000 index
3. Click `Rebuild` or press the `CTRL + R` key combination
4. Follow steps of the rebuild wizard

### Exporting Files

1. Right click `All` in the extensions list and click `Export`

or

1. Locate the specific extension you want to export and right click it and click `Export`

or

1. Search for or otherwise scroll to and select (you may select more than 1) then right click after selecting desired files and click `Export`

### Comparing files

1. Search for or scroll to the desired file
2. Right click the file grid and click `Compare`

**!!! It should be noted, this currently will not function for rdb due to the header date changing! !!!**

### Inserting/Deleting files

1. Search for or scroll to the desired file (for delete)
2. Right click the grid and select Insert/Delete
3. Follow prompts

**You can also simply drag new files (to be inserted) directly onto a loaded data index**

## Using RDB Utility

*Most of the instructions below assume you have an open and properly configured instance of Grimoire*

### Opening RDB

RDB data can be loaded in multiple ways:

1. Click the `Load` button
2. Click the `File` button and select the desired .rdb

or

1. Locate the .rdb in in Windows Explorer
2. Drag it onto a clean .rdb tab

or

1. Press `CTRL + O` to open File Dialogue
2. Select the desired .rdb

or

1. Locate client directory containing desired .rdb
2. Load the index (data.000) in the same way a normal rdb would be loaded
3. Select the desired RDB from the list of selectable rdb (contained in this client)

or

1. Click the `Load` button
2. Click the `SQL` button

### Saving RDB

Before saving you should make sure you have (ascii) / encrypted (buttons on the toolbar of the rdb tab) are set properly.

1. Click the `Save` button
2. Hover mouse over `File`
3. Select appropriate type to save as. 

or

1. Press the `CTRL + S` key combination

### Searching

To find a specific value in a specific field, simply press `CTRL + F`. Select the desired field and enter the desired value to be matched, then press OK. If the value exists, it will be automatically selected.

### Sorting

Click the column header to sort, clicking it again will reverse the sort order.

### Editing Bit Flags

Grimoire has the ability to launch the Bit Flag Utility from within an rdb tab instance, to enable this simply see the example given in the ItemResource72.lua (structure)

```lua
{"item_use_flag", INT32, flag=BIT_FLAG, opt={ "use_flags_under_81" }},
```

The bit flag utility accepts any 4 byte integer as a value, so if you have a field such as the "item_use_flag" who has the fieldtype "INT32" you can add the flag "BIT_FLAG" to it, this specifies that when you right click on this field in a loaded RDB tab, you can then click "Open w/ Flag Editor"

You can go further than this by specifying the opt variable and giving it the name of the default flag list (.txt) we want to be loaded to process the value in the current cell.

## Using Hash Utility

*You should make sure you have properly configured the Hash Utility related settings*

### Single Name (Real Time) Hashing

1. Locate the 'Instant' group of controls. (Upper left hand corner of the tab)
2. Enter a plain-text or hashed name

The opposite will be generated in the 'Output' textbox. *(You can also use the flip button to flip the results)*

### Multiple File Hashing

You can load files to be converted into the Hash Utility in multiple ways:

1. Locate the file'(s) in Windows Explorer
2. Select the file'(s) and drag them onto the 'Multi-File' grid
3. (If 'Auto-Convert' is not enabled) Right click the grid and hover over 'Convert'
4. Click `All`

or

1. Locate the folder containing files to be hashed in Windows Explorerer
2. Drag the folder onto the `Multi-File` grid
3. (If 'Auto-Convert' is not enabled) Right click the grid and hover over `Convert`
4. Click `All`

### Add/Remove (ascii)

During the name conversion (hashing) process, you can append (ascii) to all files being convert by simply enabling the 'Append (ascii)' radio button in the 'Name Options' groupbox.

## Using Bit Flag Editor

1. Open the Bit Flag Editor from the `Utilities` menu
2. Select the desired flag list
3 Modify flag selection as desired
	1. Copy output
	2. Paste flag into the `Input/Output` text box to decode


## Using Dump Updater

*(Make sure that you have configured settings related to this utility before use!)*

1. Open the Dump Updater from the `Utilities` menu
	1. Verify the `Dump Directory` is the desired directory
2. Drag-N-Drop Folder *(or files)* onto the utility
	1. Check the listed files and remove any unwanted/accidental additions
3. Click `Copy` then confirm you want to start the process!

# Using SPR Generator

1. Open the SPR Generator from the `Utilities` menu
2. Select the type of SPR you want to generate
3. Click `Generate`

*(The utility will compare item/skill/state icon listings in the database to the dump directory listed in your settings to determine if a matching icon actually exists. If a matching icon is not found the icon will not be listed in the generated spr!)*

# Using XOR Editor

1. Open the XOR Editor from the `Utilities` menu
2. Load XOR Key
	1. You can load the `Default` key, a key stored in a `key file` or the key stored in your `Config.json` if applicable
3. Edit the key as desired
	1. The `unResourceEncodeKey` is updated in real time
	2. If generating a new key for the sframe, you can right click the hex editor and click `Copy` and simply paste the copied output into the `XORen.cpp::szResourceEncodeKey` *(do the same for the `unResourceEncodeKey`)*
4. Save the key
	1. You can save the key directly to the `Config.json` as-well as a `key file` *(which can be shared with developers on your team)*
	2. Generated `key files` do not automatically save into the `Config.json`!!! *(This goes for loading them as-well!)*

# Using Modified XOR Key

If you have modified the `szResourceEncodeKey` / `unResourceEncodeKey` of your `SFrame.exe` `XORen.cpp` you will need to provide this modified key to Grimoire in order to create and load custom data.xxx files. 
