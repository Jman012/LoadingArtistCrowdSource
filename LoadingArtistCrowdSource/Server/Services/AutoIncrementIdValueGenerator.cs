using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace LoadingArtistCrowdSource.Server.Services
{
	public class AutoIncrementIdValueGenerator<TEntity, TGroupKey> : ValueGenerator<int> where TEntity: class
	{
		public override bool GeneratesTemporaryValues => false;

		private readonly MemberInfo _groupKeyMemberInfo;
		private readonly Expression<Func<TEntity, int?>> _identityKeyExpression;

		public AutoIncrementIdValueGenerator(
			Expression<Func<TEntity, TGroupKey>> groupKeyExpression, 
			Expression<Func<TEntity, int?>> identityKeyExpression)
		{
			_groupKeyMemberInfo = ((MemberExpression)groupKeyExpression.Body).Member;
			_identityKeyExpression = identityKeyExpression;
		}

		public override int Next(EntityEntry entry)
		{
			TEntity model = (entry.Entity as TEntity)!;
			
			var param = Expression.Parameter(typeof(TEntity));
			var whereGroupKeyEqualsExpr = Expression.Lambda<Func<TEntity, bool>>(
				Expression.Equal(
					Expression.MakeMemberAccess(param, _groupKeyMemberInfo), 
					Expression.MakeMemberAccess(
						Expression.Constant(model),
						_groupKeyMemberInfo
					)
				),
				param
			);

			var latest = entry.Context.ChangeTracker
				.Entries()
				.Where(e => e.State == EntityState.Added && e.Entity is TEntity)
				.Select(e => (e.Entity as TEntity)!)
				.Where(whereGroupKeyEqualsExpr.Compile())
				.Max(_identityKeyExpression.Compile());
			if (latest.HasValue)
			{
				return latest.Value + 1;
			}

			latest = entry.Context
				.Set<TEntity>()
				.Where(whereGroupKeyEqualsExpr)
				.Max(_identityKeyExpression);
			return (latest ?? 0) + 1;
		}

		public override async ValueTask<int> NextAsync(EntityEntry entry, CancellationToken cancellationToken = default)
		{
			TEntity model = (entry.Entity as TEntity)!;
			
			var param = Expression.Parameter(typeof(TEntity));
			var whereGroupKeyEqualsExpr = Expression.Lambda<Func<TEntity, bool>>(
				Expression.Equal(
					Expression.MakeMemberAccess(param, _groupKeyMemberInfo), 
					Expression.MakeMemberAccess(
						Expression.Constant(model),
						_groupKeyMemberInfo
					)
				),
				param
			);

			var latest = entry.Context.ChangeTracker
				.Entries()
				.Where(e => e.State == EntityState.Added && e.Entity is TEntity)
				.Select(e => (e.Entity as TEntity)!)
				.Where(whereGroupKeyEqualsExpr.Compile())
				.Max(_identityKeyExpression.Compile());
			if (latest.HasValue)
			{
				return latest.Value + 1;
			}

			latest = await entry.Context
				.Set<TEntity>()
				.Where(whereGroupKeyEqualsExpr)
				.MaxAsync(_identityKeyExpression);
			return (latest ?? 0) + 1;
		}
	}
}