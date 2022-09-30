﻿using System;
using System.Linq.Expressions;

namespace SuperPastel.Nucleo.Especificacoes
{
    public sealed class AndSpecification<T> : CompositeSpecification<T> where T : class
    {
        #region Members

        private ISpecification<T> _RightSideSpecification = null;
        private ISpecification<T> _LeftSideSpecification = null;

        #endregion

        #region Public Constructor

        /// <summary>
        /// Default constructor for AndSpecification
        /// </summary>
        /// <param name="leftSide">Left side specification</param>
        /// <param name="rightSide">Right side specification</param>
        public AndSpecification(ISpecification<T> leftSide, ISpecification<T> rightSide)
        {
            _LeftSideSpecification = leftSide ?? throw new ArgumentNullException("leftSide");
            _RightSideSpecification = rightSide ?? throw new ArgumentNullException("rightSide");
        }

        #endregion

        #region Composite Specification overrides

        /// <summary>
        /// Left side specification
        /// </summary>
        public override ISpecification<T> LeftSideSpecification
        {
            get { return _LeftSideSpecification; }
        }

        /// <summary>
        /// Right side specification
        /// </summary>
        public override ISpecification<T> RightSideSpecification
        {
            get { return _RightSideSpecification; }
        }

        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.Specification.ISpecification{T}"/>
        /// </summary>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.Specification.ISpecification{T}"/></returns>
        public override Expression<Func<T, bool>> SatisfiedBy()
        {
            Expression<Func<T, bool>> left = _LeftSideSpecification.SatisfiedBy();
            Expression<Func<T, bool>> right = _RightSideSpecification.SatisfiedBy();

            return (left.And(right));

        }

        #endregion
    }
}
