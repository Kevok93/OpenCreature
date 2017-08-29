rm creature.db 2>/dev/null
sqlite3 ":memory:" ".read db_sql_statements/creature.db.schema.sql" ".read db_sql_statements/creature.db.values.sql" ".save creature.db"
