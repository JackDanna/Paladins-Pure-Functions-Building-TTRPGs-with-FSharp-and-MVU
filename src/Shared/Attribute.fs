module Attribute

// Model
type Attribute = { AttributeName: string; Level: int }

let init name = { AttributeName = name; Level = 0 }

// Update
type Msg =
    | ModifyName of string
    | AddOneToLevel
    | MinusOneToLevel

let update (msg: Msg) (model: Attribute) =

    match msg with
    | ModifyName newName -> { model with AttributeName = newName }
    | AddOneToLevel -> { model with Level = model.Level + 1 }
    | MinusOneToLevel -> { model with Level = model.Level - 1 }