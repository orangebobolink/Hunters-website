import { User } from "@/entities/user/models/User";
import { Message } from "./Message";
 
export interface Group {
    id:string,
    users:User[],
    messages: Message[]
}