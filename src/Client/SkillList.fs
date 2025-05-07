module SkillList

open Skill

// Model
type SkillList = Skill List

let init skillNameList = List.map Skill.init skillNameList

// Update
type Msg = SkillMsgAtPosition of skillMsg: Skill.Msg * position: int

let update (msg: Msg) (model: SkillList) =
    match msg with
    | SkillMsgAtPosition(msg: Skill.Msg, position: int) ->
        model
        |> List.mapi (fun index skill -> if index = position then Skill.update msg skill else skill)

// View
open Feliz
open Feliz.DaisyUI

let view (model: SkillList) (dispatch: Msg -> unit) =

    model
    |> List.mapi (fun index skill -> Skill.view skill (fun msg -> SkillMsgAtPosition(msg, index) |> dispatch))

//Skill.view skill (fun msg -> SkillMsgAtPosition(msg, index) |> dispatch)