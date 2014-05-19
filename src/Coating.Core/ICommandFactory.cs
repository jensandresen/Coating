namespace Coating
{
    public interface ICommandFactory
    {
        SqlCommand CreateInsertCommandFor(string id, string data, string typeName);
        SqlCommand CreateUpdateCommandFor(string id, string theNewData, string typeName);
        SqlCommand CreateDeleteCommandFor(string id);
        SqlCommand CreateSelectByIdCommandFor(string id);
        SqlCommand CreateSelectByTypeCommandFor(string typeName);
    }
}