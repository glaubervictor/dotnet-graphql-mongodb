using System;
using System.Linq.Expressions;

namespace SuperPastel.Nucleo.Especificacoes
{
    public sealed class DirectSpecification<TEntity> : Specification<TEntity> where TEntity : class
    {
        #region Members

        Expression<Func<TEntity, bool>> _MatchingCriteria;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor for Direct Specification
        /// </summary>
        /// <param name="matchingCriteria">A Matching Criteria</param>
        public DirectSpecification(Expression<Func<TEntity, bool>> matchingCriteria)
        {
            if (matchingCriteria == (Expression<Func<TEntity, bool>>)null)
                throw new ArgumentNullException("matchingCriteria");

            _MatchingCriteria = matchingCriteria;
        }

        #endregion

        #region Override

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override Expression<Func<TEntity, bool>> SatisfiedBy()
        {
            return _MatchingCriteria;
        }

        #endregion
    }
}
