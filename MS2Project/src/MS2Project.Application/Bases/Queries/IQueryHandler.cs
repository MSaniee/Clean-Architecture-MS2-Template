namespace MS2Project.Application.Bases.Queries;

public interface IQueryHandler<in TQuery, TResult> :
       IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>
{

}

