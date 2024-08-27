namespace NewsFeedApp

open System.Collections.Generic
open System.Threading.Tasks

type INewsRepository =
    abstract member GetAllNews: unit -> Task<seq<News>>
    abstract member GetNewsById: int -> Task<News option>
    abstract member AddNews: News -> Task<int>
    abstract member UpdateNews: News -> Task
    abstract member DeleteNews: int -> Task
    abstract member Upvote: int -> Task
    abstract member Downvote: int -> Task

type NewsRepository() =
    let newsStorage = Dictionary<int, News>()
    let mutable currentId = 0

    interface INewsRepository with
        member _.GetAllNews() =
            Task.FromResult(newsStorage.Values :> seq<News>)

        member _.GetNewsById(id) =
            Task.FromResult(
                if newsStorage.ContainsKey(id) then
                    Some(newsStorage.[id])
                else
                    None
            )

        member _.AddNews(news) =
            currentId <- currentId + 1
            let newNews = { news with Id = currentId }
            newsStorage.[currentId] <- newNews
            Task.FromResult(currentId)

        member _.UpdateNews(news) =
            if newsStorage.ContainsKey(news.Id) then
                newsStorage.[news.Id] <- news
            Task.CompletedTask

        member _.DeleteNews(id) =
            if newsStorage.ContainsKey(id) then
                newsStorage.Remove(id) |> ignore
            Task.CompletedTask

        member _.Upvote(id) =
            if newsStorage.ContainsKey(id) then
                let news = newsStorage.[id]
                newsStorage.[id] <- { news with Upvotes = news.Upvotes + 1 }
            Task.CompletedTask

        member _.Downvote(id) =
            if newsStorage.ContainsKey(id) then
                let news = newsStorage.[id]
                newsStorage.[id] <- { news with Downvotes = news.Downvotes + 1 }
            Task.CompletedTask
