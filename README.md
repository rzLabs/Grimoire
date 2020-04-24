# Grimoire
All in one minimalistic and purpose driven Rappelz Client/RDB Editing Suite.

# Features
1. Highly configurable
2. In-Depth, detailed logging system
3. Utilitarian, ease to use design
4. Lightweight

# Current Functionality
1. **RDB Editing**
  * User defined structures (LUA via MoonSharp)
  	* Definable header (.ref)
  * Load from File/SQL AND Data.XXX
  	* Open w/ CTRL+O or File > Load
	* Load by Drag-N-Drop
  * Save to File/SQL
	* Configurable select/insert statements via sqlColumns, selectStatement properties
  * Sort populated grid (Ascecnding, Descending) by clicking desired column header
  * Search populated grid (CTRL+F) by selecting name and entering a value to match.
	
2. **Data Management**
  * Load
    * File Menu
    * Drag-N-Drop
    * CTRL+O
  * View client files
  * View detailed information per extension
    * Extension file count
    * Extension total size consumed
  * View detailed information per file
    * Data.00* ID
    * File offset
    * File Size (length)
    * File Encryption status
    * File Extension
    * File Upload Path (for use with retail upload system)
  * Filter index by Extension
  * Export Files
    * Export by Extension
		* Export all files by 'ALL' Extension
    * Export Selected Files
  * Insert Files
	* Right Click 'Insert' 
	    * Multi-Select capable
	* Drop Folder/File'(s)
  * Search
    * All Files
    * Extension (by selecting extension then searching)
  * Compare Files (Stored vs Local Disk File)
  * Rebuild (Via Rebuild Wizard)
	* View Current Size | File Count | Data Size | Data Fragmentation %
	* View Visual Presentation of Sized vs Fragmentation Size via Pie Chart
	* Statistics and Pie Chart update in real-time during rebuild process
	
3. **Hasher Utility**
  * Hash name (on the fly) w/ flip option
  * Add File or Folder via Drag-N-Drop/Right-Click Context menu
  * Add/Remove (ascii) to/from added files
  * Auto-Convert added files
  * Auto-Clear file list upon conversion
  
4. **Flag utility**
  * Supports all bit flags stored in 4 byte int (StateResource, ItemResource)
  * Selectable flag file in-tab
  * Customizable flag list
  * Included premade lists for various epics
  * Auto convert
  * Calculate AND Reverse calculations

# Planned functionality
* Admin Utility Tab
* Map Editor Utility Tab
* RDB Glandu2 Struct Importer GUI

# !!!Warning!!!

Grimoire requires the latest dependancies be referenced!
  * DataCore ( http://github.com/ismokedrow/datacore )
  * Daedalus ( https://github.com/iSmokeDrow/Daedalus )

Development resumed 4/24/2020
