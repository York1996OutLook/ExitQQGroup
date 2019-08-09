Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace Repair.Enum

	<System.Flags>
	Enum LoadLibraryFlags As UInteger
		None = 0
		DONT_RESOLVE_DLL_REFERENCES = &H1
		LOAD_IGNORE_CODE_AUTHZ_LEVEL = &H10
		LOAD_LIBRARY_AS_DATAFILE = &H2
		LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE = &H40
		LOAD_LIBRARY_AS_IMAGE_RESOURCE = &H20
		LOAD_WITH_ALTERED_SEARCH_PATH = &H8
		LOAD_LIBRARY_SEARCH_USER_DIRS = &H400
	End Enum
End Namespace
