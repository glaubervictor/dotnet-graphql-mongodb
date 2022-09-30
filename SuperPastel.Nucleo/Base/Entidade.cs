using MongoDB.Bson.Serialization.Attributes;
using SuperPastel.Nucleo.Mensageria;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperPastel.Nucleo.Base
{
    public abstract class Entidade<T> where T : Entidade<T>
    {
        private readonly IMediatorHandler _bus;

        [BsonId]
        public Guid Id { get; set; }
        public DateTime DataCadastro { get; private set; } = DateTime.Now;
        public bool Excluido { get; set; } = false;

        [BsonIgnore]
        [NotMapped]
        public IMediatorHandler Bus { get { return _bus; } }

        public Entidade(IMediatorHandler bus) {
            _bus = bus;
            Id = Guid.NewGuid();
        }

        public Entidade(Guid id)
        {
            Id = id;
        }

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entidade<T>;

            if (ReferenceEquals(this, compareTo))
            {
                return true;
            }

            if (compareTo is null) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator == (Entidade<T> a, Entidade<T> b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator != (Entidade<T> a, Entidade<T> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return GetType().Name + "[Id = " + Id + "]";
        }
    }
}
