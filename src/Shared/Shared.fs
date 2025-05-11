namespace Shared

open System

type Todo = { Id: Guid; Description: string }

module Todo =
    let isValid (description: string) =
        String.IsNullOrWhiteSpace description |> not

    let create (description: string) = {
        Id = Guid.NewGuid()
        Description = description
    }

type ITodosApi = {
    getTodos: unit -> Async<Todo list>
    addTodo: Todo -> Async<Todo list>
}

type User = | Guest

open Character

module Bridge =
    open System

    type ClientToServerMsg = UpdateCharacter of Character.Msg * echoGuid: Guid

    type ServerToClientMsg =
        | InitialConnection of Character
        | BroadcastedCharacterMsg of Character.Msg * echoGuid: Guid

    let endpoint = "/bridge"