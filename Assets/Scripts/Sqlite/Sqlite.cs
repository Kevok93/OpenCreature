using System;
using System.Runtime.InteropServices;
using System.Linq;
public static class Sqlite {
    [DllImport ("sqlite3")]
	public static extern SqliteErrorCode sqlite3_open_v2(byte[] filename, ref IntPtr db, SqliteOpenOpts flags, byte[] VFS);

	[DllImport ("sqlite3")]
	public static extern SqliteErrorCode sqlite3_prepare_v2(IntPtr db, byte[] sql, int size, ref IntPtr statement, ref IntPtr leftovers);

	[DllImport ("sqlite3")]
	public static extern SqliteErrorCode sqlite3_finalize(IntPtr statement);

	[DllImport ("sqlite3")]
	public static extern SqliteErrorCode sqlite3_step(IntPtr statement);

	[DllImport ("sqlite3")]
	public static extern IntPtr sqlite3_column_blob(IntPtr statement, int ColNum);

	[DllImport ("sqlite3")]
	public static extern IntPtr sqlite3_column_text(IntPtr statement, int ColNum);

	[DllImport ("sqlite3")]
	public static extern int sqlite3_column_bytes(IntPtr statement, int ColNum);

	[DllImport ("sqlite3")]
	public static extern IntPtr sqlite3_column_name(IntPtr statement, int ColNum);

	[DllImport ("sqlite3")]
	public static extern int sqlite3_column_count(IntPtr statement);
	
	[DllImport ("sqlite3")]
	public static extern SqliteErrorCode sqlite3_close_v2(IntPtr db);
	
	[DllImport ("sqlite3")]
	public static extern SqliteDatatype sqlite3_column_type(IntPtr statement, int ColNum);

	[DllImport ("sqlite3")]
	public static extern int sqlite3_column_int(IntPtr statement, int ColNum);

	[DllImport ("sqlite3")]
	public static extern float sqlite3_column_float(IntPtr statement, int ColNum);

	[DllImport ("sqlite3")]
	public static extern IntPtr sqlite3_errmsg(IntPtr db);

	[DllImport ("sqlite3")]
	public static extern IntPtr sqlite3_errstr(IntPtr db);

	[DllImport ("sqlite3")]
	public static extern SqliteErrorCode sqlite3_get_table(IntPtr db, byte[] sql, out IntPtr results, out int rows, out int cols, out byte[] err);
	
	[DllImport ("sqlite3")]
	public static extern void sqlite3_free_table(IntPtr tableResult);
}