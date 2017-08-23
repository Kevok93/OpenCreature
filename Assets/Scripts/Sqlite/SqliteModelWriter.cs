using UnityEngine;
using System;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

public class SqliteModelWriter : MonoBehaviour {
	public IntPtr null_ptr = (IntPtr)0;
    public Mesh source_mesh;
    public string output_file;
	public void start() {
        if (output_file == null) return;
        if (System.IO.File.Exists(output_file)) System.IO.File.Delete(output_file);
		IntPtr db = null_ptr;
		SqliteErrorCode retc = Sqlite.sqlite3_open_v2(
			Encoding.Default.GetBytes(output_file), 
			ref db,
			SqliteOpenOpts.SQLITE_OPEN_READONLY,
			null
		);
		if (db == null_ptr) return;
		var watch = new System.Diagnostics.Stopwatch();
		watch.Start();
		writeVerticies (db, source_mesh.vertices);
		Console.Out.WriteLine("Wrote Verticies: "+watch.ElapsedMilliseconds);
		watch.Reset(); watch.Start();
		writePolygons (db, source_mesh.triangles);
		Console.Out.WriteLine("Wrote Polys: "+watch.ElapsedMilliseconds);
		watch.Stop();
		if (db != null_ptr) Sqlite.sqlite3_close (db);
	}
    public static SqliteErrorCode writeVerticies(IntPtr db, Vector3[] vertices) {
        string query = @"
            CREATE TABLE vertices ( 
                vertex_id	INTEGER NOT NULL,
                x        	REAL NOT NULL DEFAULT 0,
                y        	REAL NOT NULL DEFAULT 0,
                z        	REAL NOT NULL DEFAULT 0,
                PRIMARY KEY(vertex_id)
             );"+"\n";
             
		for (int i = 0; i < vertices.Count; i++) {
            Vector3 vertex = vertices[i];
            query += "INSERT INTO vertices VALUES(" +
                i + "," +
		        vertex.x + "," + 
		        vertex.y + "," + 
		        vertex.z + ");\n";
		}
        Console.Out.WriteLine(query);
		
		IntPtr prep_stmt = null_ptr,
		leftovers = null_ptr;
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
             
		for (int i = 0; i < polygons.Count; i+=3) {
            query += "INSERT INTO polygons VALUES(" +
                polygons[i+0] + "," +
		        polygons[i+1] + "," + 
		        polygons[i+2] + ");\n";
		}
        Console.Out.WriteLine(query);
		IntPtr prep_stmt = null_ptr,
		leftovers = null_ptr;
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
