using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using opencreature;

public class SqliteConnection : AbstractDatabase {
	const string PREFIX = "SQLITE";
	protected IntPtr db;
	public static IntPtr null_ptr = (IntPtr)0;
	public static char[] delim_semi = {';'};
	public static new log4net.ILog log;
	static SqliteConnection() {
		log = log4net.LogManager.GetLogger(PREFIX);
	}
	
	
	public SqliteConnection(string path, SqliteOpenOpts mode = SqliteOpenOpts.SQLITE_OPEN_READONLY) {
        if (!System.IO.File.Exists(path)) throw new FileNotFoundException(path);
		SqliteErrorCode retc = Sqlite.sqlite3_open_v2(
			Encoding.Default.GetBytes(path), 
			ref db,
			mode,
			null
		);
        if (retc != SqliteErrorCode.SQLITE_OK) {
            db = null_ptr;
            throw new InvalidOperationException("Error opening SQLITE Database: SQLITE exited with code " + retc + "\n" + getError());
        }
	}
	
	public override List<List<Dictionary<string,string>>> sql(string query) {
		if (db == null_ptr) return null;
		
		var results = new List<List<Dictionary<string, string>>>();
		
		string[] querytok = query.Split(delim_semi,StringSplitOptions.RemoveEmptyEntries);
		
		foreach (string subquery in querytok) {
			string subquery_mod = subquery+ ";";
			
            log.Trace(subquery_mod);
            results.Add(getTable(subquery_mod));
		} 
					
		return results;
	}
	
	public override List<List<Dictionary<string,string>>> this[string query] {
		get{return sql(query);}
	}
	/*
	private List<Dictionary<string,string>> getTable(string sql) {
        int sizeofptr = Marshal.SizeOf(typeof(IntPtr));
        int cols, rows;
        IntPtr results_c;
        byte[] error_c;
        SqliteErrorCode retc;
        
        retc = Sqlite.sqlite3_get_table(
            db,
            Encoding.Default.GetBytes(sql),
            out results_c,
            out rows,
            out cols,
            out error_c
        );
        if (retc != SqliteErrorCode.SQLITE_OK || error_c != null) {
            throw new ArgumentException("Error reading table; SQLITE exited with code " + retc + "\n " + Encoding.Default.GetString(error_c) + "\n" + getError());
        }
        
        string[] keys = new string[cols];
        if (rows < 1) return new List<Dictionary<string, string>>(0);
        List<Dictionary<string,string>> result = new List<Dictionary<string, string>>(rows-1);
        for (int i = 0; i < cols; i++) {
            keys[i] = Marshal.PtrToStringAnsi(Marshal.ReadIntPtr(results_c, i * sizeofptr));
        }
        for (int i = cols; i < rows*cols+cols; i+=cols) {
            var row = new Dictionary<string,string>(cols);
            for (int j = 0; j < cols; j++) {
                row[keys[j]] = Marshal.PtrToStringAnsi(Marshal.ReadIntPtr(results_c, (i+j) * sizeofptr));
            }
            result.Add(row);
        }
        Sqlite.sqlite3_free_table(results_c);
        return result;
	}
	*/
	private unsafe List<Dictionary<string,string>> getTable(string sql) {
		int cols, rows;
		IntPtr results_c;
		byte[] error_c;
		SqliteErrorCode retc;

		IntPtr stmt = null_ptr;
		IntPtr useless_tail = null_ptr;
		List<Dictionary<string, string>> result = null;
	
		retc = Sqlite.sqlite3_prepare_v2(
			db,
			Encoding.Default.GetBytes(sql),
			Encoding.Default.GetBytes(sql).Length + 1,
			ref stmt,
			ref useless_tail
		);
		if (retc != SqliteErrorCode.SQLITE_OK) {
			throw new ArgumentException("Error reading table; SQLITE exited with code " + retc + "  " + getError());
		}
		Sqlite.sqlite3_finalize(stmt);

		retc = Sqlite.sqlite3_get_table(
			db,
			Encoding.Default.GetBytes(sql),
			out results_c,
			out rows,
			out cols,
			out error_c
		);
		int dataCount = cols + rows * cols;
		if (retc != SqliteErrorCode.SQLITE_OK || error_c != null) {
			throw new ArgumentException(
				"Error reading table; SQLITE exited with code " + retc + "\n " + Encoding.Default.GetString(error_c) + "\n" +
				getError());
		}
		sbyte** results_array = (sbyte**)results_c.ToPointer();

		string[] keys = new string[cols];
		if (rows < 1) return new List<Dictionary<string, string>>(0);
		result = new List<Dictionary<string, string>>(rows - 1);
		for (int i = 0; i < cols; i++) { keys[i] = new String(results_array[i]); }
		for (int i = cols; i < dataCount; i += cols) {
			var row = new Dictionary<string, string>(cols);
			for (int j = 0; j < cols; j++) { row[keys[j]] = new String(results_array[i + j]); }
			result.Add(row);
		}
		Sqlite.sqlite3_free_table(results_c);
		return result;
	}

	~SqliteConnection() {
		try {Sqlite.sqlite3_close_v2(db);} catch (Exception e) { Console.Out.WriteLine(e); }
		db = null_ptr;
	}

	
	public override string getError() {
        return Marshal.PtrToStringAnsi(Sqlite.sqlite3_errmsg(db)) + "\n" + Marshal.PtrToStringAnsi(Sqlite.sqlite3_errstr(db));
	}
}