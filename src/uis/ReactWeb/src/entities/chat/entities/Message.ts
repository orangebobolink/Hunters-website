export interface Message {
    id?:string,
    userId:string,
    content: string,
    createTime?:Date,
    toUserId?:string,
    groupId:string
}