// These are the options to the internal sqlite3_config call.
public enum SqliteOpenOpts
{
	SQLITE_OPEN_READONLY = 1,  // Ok for sqlite3_open_v2()
	SQLITE_OPEN_READWRITE = 2,  // Ok for sqlite3_open_v2()
	SQLITE_OPEN_CREATE = 4,  // Ok for sqlite3_open_v2()
	SQLITE_OPEN_DELETEONCLOSE = 8,  // VFS only
	SQLITE_OPEN_EXCLUSIVE = 16,  // VFS only
	SQLITE_OPEN_AUTOPROXY = 32,  // VFS only
	SQLITE_OPEN_URI = 64,  // Ok for sqlite3_open_v2()
	SQLITE_OPEN_MEMORY = 128,  // Ok for sqlite3_open_v2()
	SQLITE_OPEN_MAIN_DB = 256,  // VFS only
	SQLITE_OPEN_TEMP_DB = 512,  // VFS only
	SQLITE_OPEN_TRANSIENT_DB = 1024,  // VFS only
	SQLITE_OPEN_MAIN_JOURNAL = 2048,  // VFS only
	SQLITE_OPEN_TEMP_JOURNAL = 4096,  // VFS only
	SQLITE_OPEN_SUBJOURNAL = 8192,  // VFS only
	SQLITE_OPEN_MASTER_JOURNAL = 16384,  // VFS only
	SQLITE_OPEN_NOMUTEX = 32768,  // Ok for sqlite3_open_v2()
	SQLITE_OPEN_FULLMUTEX = 65536,  // Ok for sqlite3_open_v2()
	SQLITE_OPEN_SHAREDCACHE = 131072,  // Ok for sqlite3_open_v2()
	SQLITE_OPEN_PRIVATECACHE = 262144,  // Ok for sqlite3_open_v2()
	SQLITE_OPEN_WAL = 524288,  // VFS only
}