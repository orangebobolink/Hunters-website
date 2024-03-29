import React from 'react';
import {LoginForm} from "@/widgets/forms/ui/login-form";


export const SignInPage: React.FC = () => {
    return (
        <div className="h-screen flex items-center justify-center flex-col">
            <LoginForm/>
        </div>
    )
}