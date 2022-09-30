using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SuperPastel.Nucleo.Constantes
{
    [JsonObject]
    public class EnumeradorSeguro<T, C> : IEnumeradorSeguro<C>, IComparable where T : IEnumeradorSeguro<C>
    {
        private Func<string> funcaoDescricao = null;

        public EnumeradorSeguro(C id, Func<string> funcao)
        {
            Id = id;
            funcaoDescricao = funcao;
        }

        public EnumeradorSeguro()
        {
        }

        public EnumeradorSeguro(C id, string nome) : this(id, () => nome)
        {
        }

        [JsonProperty]
        public C Id { get; protected set; }

        [JsonProperty]
        public string Nome { get => funcaoDescricao.Invoke(); set => funcaoDescricao = () => value; }

        public static T Obter(C id)
        {
            foreach (T item in ObterTodosEnumerable())
            {
                if (item.Id.Equals(id)) return item;
            }

            return default;
        }

        public static List<T> ObterTodos() => ObterTodosEnumerable().ToList();

        public static IEnumerable<T> ObterTodosEnumerable()
        {
            foreach (var campo in typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public)) yield return (T)campo.GetValue(null);
        }

        public int CompareTo(object obj)
        {
            var toCompare = (T)obj;

            if (Id.Equals(toCompare.Id)) return 0;
            var orderList = new List<string>() { Id.ToString(), toCompare.Id.ToString() };
            orderList = orderList.OrderBy(c => c).ToList();
            var thisIsGrattan = orderList.IndexOf(Id.ToString()) > orderList.IndexOf(toCompare.Id.ToString());
            if (thisIsGrattan) return -1;
            return 1;
        }

        public override bool Equals(object obj) => (obj as EnumeradorSeguro<T, C>) != null && ((EnumeradorSeguro<T, C>)obj).Id.Equals(Id);

        public bool Equals(EnumeradorSeguro<T, C> obj) => (obj != null) && obj.Id.Equals(Id);

        public override int GetHashCode() => Id.GetHashCode();

        public override string ToString() => Nome;
    }
}
