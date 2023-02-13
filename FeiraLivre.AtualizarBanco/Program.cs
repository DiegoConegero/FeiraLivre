using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using MongoDB.Driver;
using System.Configuration;

namespace FeiraLivre.AtualizarBanco
{
    class Program
    {
        const string nomeFeira      = "NOME_FEIRA";
        const string bairroFeira    = "BAIRRO";
        const string regiaoFeira    = "REGIAO5";
        const string distritoFeira  = "DISTRITO";

        static void Main(string[] args)
        {
            var filePath        = $@"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName}\DEINFO_AB_FEIRASLIVRES_2014.xls";
            var con             = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={filePath};Extended Properties='Excel 8.0;HDR=YES;'";
            var sheet           = "FEIRAS LIVRES_2014";
            var conexaoHdrNo    = new OleDbConnection(string.Format(con));

            conexaoHdrNo.Open();

            var cmd = conexaoHdrNo.CreateCommand();

            cmd.CommandText = $"SELECT * FROM[{sheet}$]";

            var connection              = CreateConnection();
            var feiraLivreCollection    = new List<FeiraLivre>();
            FeiraLivre feiraLivre;

            using (OleDbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    feiraLivre          = new FeiraLivre();

                    feiraLivreCollection.Add(new FeiraLivre
                    {
                        Nome        = dr[nomeFeira].ToString(),
                        Bairro      = dr[bairroFeira].ToString(),
                        Regiao      = dr[regiaoFeira].ToString(),
                        Distrito    = dr[distritoFeira].ToString()
                    });
                }

                connection.InsertMany(feiraLivreCollection);
            }
        }

        static IMongoCollection<FeiraLivre> CreateConnection()
        {
            var connString      = ConfigurationManager.AppSettings["connectionString"].ToString();
            var dataBase        = ConfigurationManager.AppSettings["databaseName"].ToString();
            var collectionName  = ConfigurationManager.AppSettings["collectionName"].ToString();

            var mongoClient     = new MongoClient(connString);
            var mongoDatabase   = mongoClient.GetDatabase(dataBase);

            return mongoDatabase.GetCollection<FeiraLivre>(collectionName);
        }
    }
}