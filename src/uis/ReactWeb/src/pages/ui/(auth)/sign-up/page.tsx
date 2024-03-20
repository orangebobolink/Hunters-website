import React from 'react';
import {RegistrationForm} from '@/widgets/forms/ui/registration-form';

export const SignUpPage: React.FC = () => {
    return (
        <div className="h-screen flex items-center justify-center flex-col">
            <RegistrationForm/>
        </div>
    )
}