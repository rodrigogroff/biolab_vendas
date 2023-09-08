using Master.Entity;
using Master.Service.Helper;
using Npgsql;
using System.Runtime;
using System;
using System.Collections.Generic;
using Master.Entity.Dto.Infra;
using Master.Repository;

namespace Master.Service.Base
{
    public partial class SrvBase
    {
        // main componentes
        public LocalNetwork Network = null;
        public DtoServiceError Error = null;
        public object CachedObject = null;

        // helpers
        private HelperCheck helperCheck { get; set; }
        private HelperEmail helperEmail { get; set; }
        private HelperHash helperHash { get; set; }
        private HelperFileManager helperFileManager { get; set; }

        public HelperCheck HelperCheck
        {
            get
            {
                if (helperCheck is null)
                {
                    helperCheck = new HelperCheck();
                }

                return helperCheck;
            }
        }

        public HelperEmail HelperEmail
        {
            get
            {
                if (helperEmail is null)
                {
                    helperEmail = new HelperEmail();
                }

                return helperEmail;
            }
        }

        public HelperHash HelperHash
        {
            get
            {
                if (helperHash is null)
                {
                    helperHash = new HelperHash();
                }

                return helperHash;
            }
        }

        public HelperFileManager HelperFileManager
        {
            get
            {
                if (helperFileManager is null)
                {
                    helperFileManager = new HelperFileManager();
                }

                return helperFileManager;
            }
        }

        // multi-thread environments

        public List<SrvBase> Environments = null;

        public void AddEnvironment(int amount)
        {
            Environments = new List<SrvBase>();
            while (amount-- > 0)
            {
                Environments.Add(new SrvBase());
            }
        }

        public SrvBase GetEnvironment(int index)
        {
            return Environments[index];
        }

        // repositories
        public NpgsqlConnection MainDb = null;

        public NpgsqlConnection GetConnection(LocalNetwork network)
        {
            MainDb = new NpgsqlConnection(network.database);
            MainDb.Open();
            return MainDb;
        }

        public void Dispose()
        {
            Error = null;
            CachedObject = null;

            if (helperHash is not null)
            {
                if (helperHash.hashUnique != null)
                {
                    helperHash.hashUnique.Clear();
                    helperHash.hashUnique = null;
                    helperHash = null;
                }
            }

            if (helperCheck is not null)
            {
                helperCheck = null;
            }

            if (helperEmail is not null)
            {
                helperEmail = null;
            }

            if (helperFileManager is not null)
            {
                helperFileManager = null;
            }

            if (Environments is not null)
            {
                foreach (var item in Environments)
                {
                    item.Dispose();
                }

                Environments.Clear();
            }

            Environments = null;

            GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.CompactOnce;
            GC.Collect();
        }
    }
}
