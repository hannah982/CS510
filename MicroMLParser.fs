MicroMLParser.fs
namespace MicroMLAstApp.Parser

module MicroMLParser =

    type Expr =
        | Var of string
        | Int of int
        | Let of string * Expr * Expr
        | Fun of string * Expr
        | App of Expr * Expr

    // Dummy parser for illustration
    let parse (input: string) : Expr =
        // Replace this with real parsing logic or combinator parser
        match input.Trim() with
        | "let x = 5 in x" -> Let("x", Int 5, Var "x")
        | "fun x -> x" -> Fun("x", Var "x")
        | _ -> Var("unparsed")

    open System.Text.Json
    open System.Text.Json.Serialization

    // Convert AST to JSON for frontend
    let rec exprToJson (expr: Expr) : JsonElement =
        let obj =
            match expr with
            | Var name -> JsonSerializer.SerializeToElement({| name = "Var"; children = [| {| name = name |} |] |})
            | Int n -> JsonSerializer.SerializeToElement({| name = "Int"; children = [| {| name = string n |} |] |})
            | Let(name, e1, e2) ->
                JsonSerializer.SerializeToElement({|
                    name = "Let"
                    children = [| exprToJson (Var name); exprToJson e1; exprToJson e2 |]
                |})
            | Fun(param, body) ->
                JsonSerializer.SerializeToElement({|
                    name = "Fun"
                    children = [| exprToJson (Var param); exprToJson body |]
                |})
            | App(e1, e2) ->
                JsonSerializer.SerializeToElement({|
                    name = "App"
                    children = [| exprToJson e1; exprToJson e2 |]
                |})
        obj
