module Character

open AttributeWithSkillsList

// Model
type Character = {
    CharacterName: string
    CharacterArtUrl: string
    AttributeWithSkillsList: AttributeWithSkillsList
}

let init () = {

    CharacterName = ""
    CharacterArtUrl = ""
    AttributeWithSkillsList =
        AttributeWithSkillsList.init [
            "Strength", [ "Athletics"; "Lift"; "Endurance" ]
            "Reflex", [ "Dexterity"; "Perception"; "Stealth" ]
            "Intelligence", [ "Knowledge"; "Logic"; "Will" ]
            "Charisma", [ "Deception"; "Intimidation"; "Persuasion" ]
        ]
}

// Update
type Msg =
    | ModifyCharacterName of string
    | ModifyCharacterUrl of string
    | AttributeListMsg of AttributeWithSkillsList.Msg
    | SetCharacterNameAndCharacterArtURL of characterName: string * characterArtUrl: string

let update (msg: Msg) (model: Character) =
    match msg with
    | ModifyCharacterName newCharacterName -> {
        model with
            CharacterName = newCharacterName
      }
    | ModifyCharacterUrl newCharacterArtUrl -> {
        model with
            CharacterArtUrl = newCharacterArtUrl
      }
    | AttributeListMsg msg -> {
        model with
            AttributeWithSkillsList = AttributeWithSkillsList.update msg model.AttributeWithSkillsList
      }
    | SetCharacterNameAndCharacterArtURL(newCharacterName, newCharacterArtUrl) -> {
        model with
            CharacterName = newCharacterName
            CharacterArtUrl = newCharacterArtUrl
      }