using System.Collections.Generic;
using System.Linq;

namespace Coating
{
    public class SqlCommand
    {
        private readonly List<SqlCommandParameter> _parameters = new List<SqlCommandParameter>();
        
        public string Sql { get; set; }

        public IEnumerable<SqlCommandParameter> Parameters
        {
            get { return _parameters; }
        }

        public void AddParameter(string name, object value)
        {
            _parameters.Add(new SqlCommandParameter
                {
                    Name = name,
                    Value = value
                });
        }

        protected bool Equals(SqlCommand other)
        {
            return _parameters.SequenceEqual(other._parameters) && string.Equals(Sql, other.Sql);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return Equals((SqlCommand) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((_parameters != null ? _parameters.GetHashCode() : 0)*397) ^ (Sql != null ? Sql.GetHashCode() : 0);
            }
        }

        public override string ToString()
        {
            string temp = Sql;

            foreach (var p in _parameters)
            {
                temp = temp.Replace(p.Name, "'" + p.Value + "'");
            }

            return temp;
        }
    }
}