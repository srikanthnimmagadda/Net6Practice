﻿namespace Books.Dal.Repository.Interfaces
{
	public interface IUnitOfWork
	{
		/// <summary>
		/// 
		/// </summary>
		ICategoryRepository Category { get; }

		/// <summary>
		/// 
		/// </summary>
		ICoverTypeRepository CoverType { get; }

		/// <summary>
		/// 
		/// </summary>
		IProductRepository Product { get; }

		/// <summary>
		/// 
		/// </summary>
		void Save();
	}
}
