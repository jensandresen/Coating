namespace Coating.Tests
{
    public class CommandFactory
    {
        public SqlCommand CreateInsertCommandFor(string id, string data, string typeName)
        {
            var cmd = new SqlCommand();
            cmd.Sql = "insert into Data(Id, Data, Type) values(@Id, @Data, @Type)";
            cmd.AddParameter("@Id", id);
            cmd.AddParameter("@Data", data);
            cmd.AddParameter("@Type", typeName);

            return cmd;
        }

        public SqlCommand CreateUpdateCommandFor(string id, string theNewData, string typeName)
        {
            var cmd = new SqlCommand();
            cmd.Sql = "update Data set Data = @Data, Type = @Type where Id = @Id";
            cmd.AddParameter("@Id", id);
            cmd.AddParameter("@Data", theNewData);
            cmd.AddParameter("@Type", typeName);

            return cmd;
        }

        public SqlCommand CreateDeleteCommandFor(string id)
        {
            var cmd = new SqlCommand();
            cmd.Sql = "delete from Data where Id = @Id";
            cmd.AddParameter("@Id", id);

            return cmd;
        }

        public SqlCommand CreateSelectByIdCommandFor(string id)
        {
            var cmd = new SqlCommand();
            cmd.Sql = "select * from Data where Id = @Id";
            cmd.AddParameter("@Id", id);

            return cmd;
        }

        public SqlCommand CreateSelectByTypeCommandFor(string typeName)
        {
            var cmd = new SqlCommand();
            cmd.Sql = "select * from Data where Type = @Type";
            cmd.AddParameter("@Type", typeName);

            return cmd;
        }
    }
}