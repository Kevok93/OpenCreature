using System;
using UnityEngine;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace opencreature {
public abstract class Model {
	public static TypeCastDictionary<string,Model> MODELS;
	public point3D[] vertices;
	public point2D[] uvVetrices;
	public tri3D[] triangles;
	public Dictionary<string,string> Metadata;
		
	private Model() {}
	protected Model(string key) {
		MODELS.Add(key,this);
	}
	
}


public class SqliteModel :Model {
	private SqliteModel(string key) : base(key) {}
		
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
		
		var watch = new System.Diagnostics.Stopwatch(); watch.Start();
		
		Dictionary<int,point3D> vertexList = readVertices (db);
		Console.Out.WriteLine("Read Vertices: "+watch.ElapsedMilliseconds);
		watch.Reset(); watch.Start();
		List<tri3D> polyList = readPolygons (db, vertexList);
		Console.Out.WriteLine("Read Polys: "+watch.ElapsedMilliseconds);
		watch.Stop();
		if (db != null_ptr) Sqlite.sqlite3_close_v2 (db);
		
		SqliteModel model = new SqliteModel("");
		model.vertices = vertexList.Values.ToArray();
		model.triangles = polyList.ToArray();
		
		return model;
	}
	public static Dictionary<int,point3D> readVertices(IntPtr db) {
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
		//string retc_string = retc.ToString();
		Dictionary<int,point3D> vertexList = null;
		if (retc == SqliteErrorCode.SQLITE_OK) {
			vertexList = new Dictionary<int,point3D> ();
			while (true) {
			    retc = Sqlite.sqlite3_step (prep_stmt);
			    if (retc != SqliteErrorCode.SQLITE_ROW) break;
                int i = Sqlite.sqlite3_column_int(prep_stmt, 0);
				float   x=Sqlite.sqlite3_column_float(prep_stmt,1),
					    y=Sqlite.sqlite3_column_float(prep_stmt,2),
					    z=Sqlite.sqlite3_column_float(prep_stmt,3);
					    vertexList[i] = new point3D(x, y, z);
			}
			Sqlite.sqlite3_finalize (prep_stmt);
			Sqlite.sqlite3_finalize (leftovers);
		}

		return vertexList;
	}
	public static Dictionary<int,point3D> readMetadata(IntPtr db) {
		string query = "Select * from metadata;";
		IntPtr prep_stmt = null_ptr,
		leftovers = null_ptr;
		SqliteErrorCode retc = Sqlite.sqlite3_prepare_v2 (
			db,
			Encoding.Default.GetBytes (query),
			query.Length,
			ref prep_stmt,
			ref leftovers
		);
		//string retc_string = retc.ToString();
		Dictionary<int,point3D> vertexList = null;
		if (retc == SqliteErrorCode.SQLITE_OK) {
			vertexList = new Dictionary<int,point3D> ();
			while (true) {
			    retc = Sqlite.sqlite3_step (prep_stmt);
			    if (retc != SqliteErrorCode.SQLITE_ROW) break;
			    string key   = Marshal.PtrToStringAnsi(Sqlite.sqlite3_column_text(prep_stmt,0));
			    string value = Marshal.PtrToStringAnsi(Sqlite.sqlite3_column_text(prep_stmt,1));
			}
			Sqlite.sqlite3_finalize (prep_stmt);
			Sqlite.sqlite3_finalize (leftovers);
		}

		return vertexList;
	}
	public static List<tri3D> readPolygons(IntPtr db, Dictionary<int,point3D> vertexList) {
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
		List<tri3D> polyList = null;

		if (retc == SqliteErrorCode.SQLITE_OK) {
			polyList = new List<tri3D> ();
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
					polyList.Add(new tri3D(
						vertexList[c1],
						vertexList[c2],
						vertexList[c3]
					));
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
			SqliteOpenOpts.SQLITE_OPEN_READWRITE | SqliteOpenOpts.SQLITE_OPEN_CREATE,
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
	
	public static SqliteErrorCode writeVertices(IntPtr db, point3D[] vertices) {
        string query = @"
            CREATE TABLE vertices ( 
                vertex_id	INTEGER NOT NULL,
                x        	REAL NOT NULL DEFAULT 0,
                y        	REAL NOT NULL DEFAULT 0,
                z        	REAL NOT NULL DEFAULT 0,
                PRIMARY KEY(vertex_id)
             );"+"\n";
             
        for (int i = 0; i < vertices.Length; i++) {
            point3D vertex = vertices[i];
            query += "INSERT INTO vertices VALUES(" +
                i + "," +
                vertex.x + "," +
                vertex.y + "," +
                vertex.z + ");\n";
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
		else return Sqlite.sqlite3_step (prep_stmt);
	}
	public static SqliteErrorCode writePolygons(IntPtr db, tri3D[] polygons) {
        string query = @"
            CREATE TABLE polygons (
                V1        	REAL NOT NULL DEFAULT 0,
                V2       	REAL NOT NULL DEFAULT 0,
                V3       	REAL NOT NULL DEFAULT 0,
                PRIMARY KEY(V1,V2,V3)
             );"+"\n";
             
        for (int i = 0; i < polygons.Length; i++) {
            query += "INSERT INTO polygons VALUES(" +
                polygons[i].a + "," +
		        polygons[i].b + "," + 
		        polygons[i].c + ");\n";
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
}
