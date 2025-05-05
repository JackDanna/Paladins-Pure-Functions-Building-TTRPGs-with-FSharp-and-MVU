module Example1

type Attribute = {
    Name: string
    Value: int
}

type Msg =
    | AddOneToValue
    | MinusOneToValue

let init () =
    {
        Name = "Strength"
        Value = 0
    }

let update (msg: Msg) (model: Attribute) =

    match msg with
    | AddOneToValue ->
        {
            model with Value = model.Value + 1
        }
    | MinusOneToValue ->
        { model with Value = model.Value - 1 }


open Feliz

let view (model: Attribute) (dispatch: Msg -> unit) =
    Html.div [

        

    ]