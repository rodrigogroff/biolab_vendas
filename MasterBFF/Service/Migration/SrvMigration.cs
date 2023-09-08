using Dapper;
using Npgsql;
using System;
using System.IO;

namespace Master.Service.Migration
{
    /*
    public partial class SrvMigration
    {
        public string database { get; set; }
        public NpgsqlConnection db { get; set; }

        public bool ExistMigration(string tag)
        {
            const string query = "select * from \"I_Migration\" where \"stTag\"=@tag";
            return db.QueryFirstOrDefault<I_Migration>(query, new { tag }) != null;
        }

        public long InsertMigration(I_Migration mdl)
        {
            const string query = "INSERT INTO \"I_Migration\" ( \"stTag\" ) VALUES ( @stTag );select currval('public.\"I_Migration_id_seq\"');";
            using (var cmd = new NpgsqlCommand(query, db))
            {
                cmd.Parameters.AddWithValue("stTag", (object)mdl.stTag ?? DBNull.Value);
                return (long)cmd.ExecuteScalar();
            }
        }

        public void Command(string query)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(query))
                {
                    using (var cmd = new NpgsqlCommand(query, db))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }

        public void BuildTables()
        {
            foreach (var line in File.ReadAllText("Repository//_CreateDB_pg.sql").Replace("\r\n", "\n").Split('\n'))
            {
                Command(line);
            }
        }

        public void Execute()
        {
            //db = new NpgsqlConnection(database);
            //db.Open();
            //BuildTables();

            // --------------------------------
            // list of custom mods here
            // --------------------------------

            //InsertIndexes_1();

            // --------------------------------
            // end
            // --------------------------------

            //db.Close();
        }

        public void InsertIndexes_1()
        {
            const string str_InsertIndexes_1 = @"
CREATE INDEX idx_client_cluster ON public.""Client"" (""fkCompany"", ""stName"");
CREATE INDEX idx_clientinteract_cluster ON public.""ClientInteract"" (""fkCompany"", ""fkClient"");
CREATE INDEX idx_clientproject_cluster ON public.""ClientProject"" (""fkCompany"", ""fkClient"", ""fkProject"");
CREATE INDEX idx_companyunit_cluster ON public.""CompanyUnit"" (""fkCompany"", ""stName"");
CREATE INDEX idx_contact_cluster ON public.""Contact"" (""fkCompany"", ""fkClient"", ""stName"");
CREATE INDEX idx_interactiontype_cluster ON public.""InteractionType"" (""fkCompany"", ""stName"");
CREATE INDEX idx_project_cluster ON public.""Project"" (""fkCompany"", ""stName"");
CREATE INDEX idx_squad_cluster ON public.""Squad"" (""fkCompany"", ""stName"");
CREATE INDEX idx_squadproject_cluster ON public.""SquadProject"" (""fkCompany"", ""fkSquad"", ""fkProject"");
CREATE INDEX idx_user_cluster ON public.""User"" (""fkCompany"", ""stName"");
CREATE INDEX idx_userprofile_cluster ON public.""UserProfile"" (""fkCompany"", ""stName"");
CREATE INDEX idx_userproject_cluster ON public.""UserProject"" (""fkCompany"", ""fkProject"",""fkUser"");
CREATE INDEX idx_userprojectprofile_cluster ON public.""UserProjectProfile"" (""fkCompany"", ""fkUser"", ""fkProject"");
CREATE INDEX idx_usersquad_cluster ON public.""UserSquad"" (""fkCompany"", ""fkSquad"",""fkUser"");
CREATE INDEX idx_usertype_cluster ON public.""UserType"" (""fkCompany"", ""stName"");
";

            var migration = new I_Migration { stTag = "InsertIndexes_1" };

            if (!ExistMigration(migration.stTag))
            {
                InsertMigration(migration);
                foreach (var line in str_InsertIndexes_1.Replace("\r\n", "\n").Split('\n'))
                {
                    Command(line);
                }
            }
        }
    }
    */
}
