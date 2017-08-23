// These are the options to the internal sqlite3_config call.
public enum SqliteConfigOps
{
	SQLITE_CONFIG_NONE = 0, // nil
	SQLITE_CONFIG_SINGLETHREAD = 1, // nil
	SQLITE_CONFIG_MULTITHREAD = 2, // nil
	SQLITE_CONFIG_SERIALIZED = 3, // nil
	SQLITE_CONFIG_MALLOC = 4, // sqlite3_mem_methods*
	SQLITE_CONFIG_GETMALLOC = 5, // sqlite3_mem_methods*
	SQLITE_CONFIG_SCRATCH = 6, // void*, int sz, int N
	SQLITE_CONFIG_PAGECACHE = 7, // void*, int sz, int N
	SQLITE_CONFIG_HEAP = 8, // void*, int nByte, int min
	SQLITE_CONFIG_MEMSTATUS = 9, // boolean
	SQLITE_CONFIG_MUTEX = 10, // sqlite3_mutex_methods*
	SQLITE_CONFIG_GETMUTEX = 11, // sqlite3_mutex_methods*
	// previously SQLITE_CONFIG_CHUNKALLOC 12 which is now unused
	SQLITE_CONFIG_LOOKASIDE = 13, // int int
	SQLITE_CONFIG_PCACHE = 14, // sqlite3_pcache_methods*
	SQLITE_CONFIG_GETPCACHE = 15, // sqlite3_pcache_methods*
	SQLITE_CONFIG_LOG = 16, // xFunc, void*
	SQLITE_CONFIG_URI = 17, // int
	SQLITE_CONFIG_PCACHE2 = 18, // sqlite3_pcache_methods2*
	SQLITE_CONFIG_GETPCACHE2 = 19, // sqlite3_pcache_methods2*
	SQLITE_CONFIG_COVERING_INDEX_SCAN = 20, // int
	SQLITE_CONFIG_SQLLOG = 21, // xSqllog, void*
	SQLITE_CONFIG_MMAP_SIZE = 22, // sqlite3_int64, sqlite3_int64
	SQLITE_CONFIG_WIN32_HEAPSIZE = 23 // int nByte
}