namespace NewsFeedApp

open System.Threading.Tasks

type INewsService =
    abstract member GetAllNews: unit -> Task<seq<News>>
    abstract member GetNewsById: int -> Task<News option>
    abstract member AddNews: News -> Task<int>
    abstract member UpdateNews: News -> Task
    abstract member DeleteNews: int -> Task
    abstract member Upvote: int -> Task
    abstract member Downvote: int -> Task

type NewsService(repository: INewsRepository) =
    interface INewsService with
        member _.GetAllNews() = repository.GetAllNews()
        member _.GetNewsById(id) = repository.GetNewsById(id)
        member _.AddNews(news) = repository.AddNews(news)
        member _.UpdateNews(news) = repository.UpdateNews(news)
        member _.DeleteNews(id) = repository.DeleteNews(id)
        member _.Upvote(id) = repository.Upvote(id)
        member _.Downvote(id) = repository.Downvote(id)
