namespace NewsFeedApp

open Microsoft.AspNetCore.Mvc
open System.Threading.Tasks

[<Route("api/[controller]")>]
[<ApiController>]
type NewsController(service: INewsService) =
    inherit ControllerBase()


    [<HttpGet>]
    member this.GetAllNews() : Task<IActionResult> =
        async {
            let! news = service.GetAllNews() |> Async.AwaitTask
            return this.Ok(news) :> IActionResult
        } |> Async.StartAsTask

    [<HttpGet("{id}")>]
    member this.GetNewsById(id: int) : Task<IActionResult> =
        async {
            let! news = service.GetNewsById(id) |> Async.AwaitTask
            match news with
            | Some n -> return this.Ok(n) :> IActionResult
            | None -> return this.NotFound() :> IActionResult
        } |> Async.StartAsTask

    [<HttpPost>]
    member this.AddNews([<FromBody>] news: News) : Task<IActionResult> =
        async {
            let! id = service.AddNews(news) |> Async.AwaitTask
            return this.Ok(id) :> IActionResult
        } |> Async.StartAsTask
        
    [<HttpPut("{id}")>]
    member this.UpdateNews(id: int, [<FromBody>] news: News) : Task<ActionResult> =
        async {
            do! service.UpdateNews(news) |> Async.AwaitTask
            return this.NoContent() :> ActionResult
        } |> Async.StartAsTask

    [<HttpDelete("{id}")>]
    member this.DeleteNews(id: int) : Task<ActionResult> =
        async {
            do! service.DeleteNews(id) |> Async.AwaitTask
            return this.NoContent() :> ActionResult
        } |> Async.StartAsTask

    [<HttpPost("{id}/upvote")>]
    member this.Upvote(id: int) : Task<ActionResult> =
        async {
            do! service.Upvote(id) |> Async.AwaitTask
            return this.Ok() :> ActionResult
        } |> Async.StartAsTask

    [<HttpPost("{id}/downvote")>]
    member this.Downvote(id: int) : Task<ActionResult> =
        async {
            do! service.Downvote(id) |> Async.AwaitTask
            return this.Ok() :> ActionResult
        } |> Async.StartAsTask
