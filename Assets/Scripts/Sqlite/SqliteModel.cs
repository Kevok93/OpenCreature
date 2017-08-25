using System;
//using System.Linq;
using System.Text;
using System.Collections.Generic;
public class SqliteModel {
    public float[][] vertices;
    public int[] triangles;
	public static IntPtr null_ptr = (IntPtr)0;
	public static SqliteModel readModel(string path) {
		IntPtr db = null_ptr;
		SqliteErrorCode retc = Sqlite.sqlite3_open_v2(
			Encoding.Default.GetBytes(path), 
			ref db,
			SqliteOpenOpts.SQLITE_OPEN_READONLY,
			null
		);
		if (retc != SqliteErrorCode.SQLITE_OK) return null;
		var watch = new System.Diagnostics.Stopwatch();
		watch.Start();
		Dictionary<int,float[]> vertexList = readVertices (db);
		Console.Out.WriteLine("Read Vertices: "+watch.ElapsedMilliseconds);
		watch.Reset(); watch.Start();
		List<int> polyList = readPolygons (db, vertexList);
		Console.Out.WriteLine("Read Polys: "+watch.ElapsedMilliseconds);
		watch.Stop();
		if (db != null_ptr) Sqlite.sqlite3_close_v2 (db);
		SqliteModel model = new SqliteModel ();
		float[][] vertexListA = new float[vertexList.Count][];
        int i = 0;
		foreach (float[] vertex in vertexList.Values) {
            vertexListA[i++] = vertex;
		}
		model.triangles = polyList.ToArray();
		return model;
	}
	public static Dictionary<int,float[]> readVertices(IntPtr db) {
		string query = "Select * from vertices;";
		IntPtr prep_stmt = null_ptr,
		leftovers = null_ptr;
		SqliteErrorCode retc = Sqlite.sqlite3_prepare_v2 (
			db,
			Encoding.Default.GetBytes (query),
			query.Length,
			ref prep_stmt,
			ref leftovers
		);
		string retc_string = retc.ToString();
		Dictionary<int,float[]> vertexList = null;
		if (retc == SqliteErrorCode.SQLITE_OK) {
			vertexList = new Dictionary<int,float[]> ();
			while (true) {
			    retc = Sqlite.sqlite3_step (prep_stmt);
			    if (retc != SqliteErrorCode.SQLITE_ROW) break;
                int i = Sqlite.sqlite3_column_int(prep_stmt, 0);
				float   x=Sqlite.sqlite3_column_float(prep_stmt,1),
					    y=Sqlite.sqlite3_column_float(prep_stmt,2),
					    z=Sqlite.sqlite3_column_float(prep_stmt,3);
                vertexList[i] = new float[] { x, y, z };
			}
			Sqlite.sqlite3_finalize (prep_stmt);
			Sqlite.sqlite3_finalize (leftovers);
		}

		return vertexList;
	}
	public static List<int> readPolygons(IntPtr db, Dictionary<int,float[]> vertexList) {
		string query = "Select * from polygons;";
		IntPtr prep_stmt = null_ptr,
		leftovers = null_ptr;
		SqliteErrorCode retc = Sqlite.sqlite3_prepare_v2 (
			db,
			Encoding.Default.GetBytes (query),
			query.Length,
			ref prep_stmt,
			ref leftovers
			);
		List<int> polyList = null;

		if (retc == SqliteErrorCode.SQLITE_OK) {
			polyList = new List<int> ();
			while (
				(retc = Sqlite.sqlite3_step (prep_stmt)) == SqliteErrorCode.SQLITE_ROW
			) {
				int	c1=Sqlite.sqlite3_column_int(prep_stmt,0),
					c2=Sqlite.sqlite3_column_int(prep_stmt,1),
					c3=Sqlite.sqlite3_column_int(prep_stmt,2);
				if (
					vertexList.ContainsKey (c1) &&
					vertexList.ContainsKey (c2) &&
					vertexList.ContainsKey (c3)
				) {
					polyList.Add (c1);
					polyList.Add (c2);
					polyList.Add (c3);
				} else return null;
			}
			Sqlite.sqlite3_finalize (prep_stmt);
			Sqlite.sqlite3_finalize (leftovers);
		}

		return polyList;
	}
	public void writeModel(string path) {
        if (path == null) return;
        if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
		IntPtr db = null_ptr;
		SqliteErrorCode retc = Sqlite.sqlite3_open_v2(
			Encoding.Default.GetBytes(path), 
			ref db,
			SqliteOpenOpts.SQLITE_OPEN_READWRITE,
			null
		);
		if (db == null_ptr) return;
		var watch = new System.Diagnostics.Stopwatch();
		watch.Start();
		writeVertices (db, vertices);
		Console.Out.WriteLine("Wrote Vertices: "+watch.ElapsedMilliseconds);
		watch.Reset(); watch.Start();
		writePolygons (db, triangles);
		Console.Out.WriteLine("Wrote Polys: "+watch.ElapsedMilliseconds);
		watch.Stop();
		if (db != null_ptr) Sqlite.sqlite3_close_v2 (db);
	}
	public static SqliteErrorCode writeVertices(IntPtr db, float[][] vertices) {
        string query = @"
            CREATE TABLE vertices ( 
                vertex_id	INTEGER NOT NULL,
                x        	REAL NOT NULL DEFAULT 0,
                y        	REAL NOT NULL DEFAULT 0,
                z        	REAL NOT NULL DEFAULT 0,
                PRIMARY KEY(vertex_id)
             );"+"\n";
             
        for (int i = 0; i < vertices.Length; i++) {
            float[] vertex = vertices[i];
            query += "INSERT INTO vertices VALUES(" +
                i + "," +
                vertex[0] + "," +
                vertex[1] + "," +
                vertex[2] + ");\n";
		}
        Console.Out.WriteLine(query);
		
		IntPtr 
    		prep_stmt = SqliteConnection.null_ptr,
    		leftovers = SqliteConnection.null_ptr;
		SqliteErrorCode retc = Sqlite.sqlite3_prepare_v2 (
			db,
			Encoding.Default.GetBytes (query),
			query.Length,
			ref prep_stmt,
			ref leftovers
		);
        if (retc != SqliteErrorCode.SQLITE_OK) return retc;
		return Sqlite.sqlite3_step (prep_stmt);
	}
    public static SqliteErrorCode writePolygons(IntPtr db, int[] polygons) {
        string query = @"
            CREATE TABLE polygons (
                V1        	REAL NOT NULL DEFAULT 0,
                V2       	REAL NOT NULL DEFAULT 0,
                V3       	REAL NOT NULL DEFAULT 0,
                PRIMARY KEY(V1,V2,V3)
             );"+"\n";
             
        for (int i = 0; i < polygons.Length; i+=3) {
            query += "INSERT INTO polygons VALUES(" +
                polygons[i+0] + "," +
		        polygons[i+1] + "," + 
		        polygons[i+2] + ");\n";
		}
        Console.Out.WriteLine(query);
		IntPtr prep_stmt = SqliteConnection.null_ptr,
		leftovers = SqliteConnection.null_ptr;
		SqliteErrorCode retc = Sqlite.sqlite3_prepare_v2 (
			db,
			Encoding.Default.GetBytes (query),
			query.Length,
			ref prep_stmt,
			ref leftovers
		);
        if (retc != SqliteErrorCode.SQLITE_OK) return retc;
		return Sqlite.sqlite3_step (prep_stmt);
	}
}
