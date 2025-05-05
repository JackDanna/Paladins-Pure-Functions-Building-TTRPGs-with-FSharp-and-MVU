module SimpleIndex

open Elmish
open SAFE
open Shared

open AttributeList

type Model = { AttributeList: AttributeList }

type Msg = AttributeListMsg of AttributeList.Msg

let init () = {
    AttributeList = AttributeList.init [ "Strength"; "Reflex"; "Intelligence"; "Charisma" ]
}

let update msg model =
    match msg with
    | AttributeListMsg msg -> {
        model with
            AttributeList = AttributeList.update msg model.AttributeList
      }

open Feliz

let view model dispatch =
    Html.section [
        prop.className "h-screen w-screen relative overflow-hidden"
        prop.children [
            // Meta viewport tag for proper mobile scaling
            Html.meta [
                prop.name "viewport"
                prop.content "width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no"
            ]

            // Background div with image and glass effect
            // Html.div [
            //     prop.className
            //         "absolute inset-0 bg-cover bg-center bg-fixed bg-no-repeat
            //     bg-white/20 backdrop-blur-sm"
            //     prop.style [ style.backgroundImageUrl "https://unsplash.it/1200/900?random" ]
            // ]

            // Content container (the rest of your UI)
            Html.div [
                prop.className "relative z-10 h-full w-full"
                prop.children [
                    Html.div [
                        prop.className "flex flex-col items-center justify-center h-full"
                        prop.children [
                            Html.div [ AttributeList.view model.AttributeList (AttributeListMsg >> dispatch) ]
                        ]
                    ]
                ]
            ]
        ]
    ]