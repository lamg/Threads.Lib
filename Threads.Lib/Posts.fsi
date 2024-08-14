namespace Threads.Lib

open System

module Posts =
  [<Struct>]
  type MediaType =
    | Text
    | Image
    | Video
    | Carousel

  [<Struct>]
  type ReplyAudience =
    | Everyone
    | AccountsYouFollow
    | MentionedOnly

  type PostParam =
    | CarouselItem
    | ImageUrl of Uri
    | MediaType of MediaType
    | VideoUrl of Uri
    | Text of string
    | ReplyTo of string
    | ReplyControl of ReplyAudience

  [<Struct>]
  type PostId = { id: string }

  [<Struct>]
  type SingleContainerError =
    | IsCarouselInSingleContainer
    | IsImageButImageNotProvided
    | IsVideoButNoVideoProvided
    | IsTextButNoTextProvided

  val internal createSingleContainer:
    baseUrl: string ->
    accessToken: string ->
    userId: string ->
    postParams: PostParam seq ->
      Async<Result<PostId, SingleContainerError>>

  [<Struct>]
  type CarouselItemContainerError =
    | MediaTypeMustbeVideoOrImage
    | IsImageButImageNotProvided
    | IsVideoButNoVideoProvided

  val internal createCarouselItemContainer:
    baseUrl: string ->
    accessToken: string ->
    userId: string ->
    postParams: PostParam seq ->
      Async<Result<PostId, CarouselItemContainerError>>

  [<Struct>]
  type CarouselContainerError =
    | MoreThan10Children
    | CarouselPostIsEmpty

  val internal createCarouselContainer:
    baseUrl: string ->
    accessToken: string ->
    userId: string ->
    children: PostId seq ->
    textContent: string option ->
      Async<Result<PostId, CarouselContainerError>>

  val internal publishContainer:
    baseUrl: string ->
    accessToken: string ->
    userId: string ->
    containerId: PostId ->
      Async<PostId>