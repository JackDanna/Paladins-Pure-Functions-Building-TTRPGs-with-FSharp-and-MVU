module Character

open AttributeWithSkillsList

// Model
type Character = {
    CharacterName: string
    AttributeWithSkillsList: AttributeWithSkillsList
}

let init () = {

    CharacterName = ""
    AttributeWithSkillsList =
        AttributeWithSkillsList.init [
            "Strength", [ "Athletics"; "Lift"; "Endurance" ]
            "Agility", [ "Acrobatics"; "Slieght Of Hand"; "Stealth" ]
            "Intelligence", [ "History"; "Arcana"; "Survival" ]
        ]
}

// Update
type Msg =
    | ModifyCharacterName of string
    | AttributeListMsg of AttributeWithSkillsList.Msg

let update (msg: Msg) (model: Character) =
    match msg with
    | ModifyCharacterName newCharacterName -> {
        model with
            CharacterName = newCharacterName
      }
    | AttributeListMsg msg -> {
        model with
            AttributeWithSkillsList = AttributeWithSkillsList.update msg model.AttributeWithSkillsList
      }