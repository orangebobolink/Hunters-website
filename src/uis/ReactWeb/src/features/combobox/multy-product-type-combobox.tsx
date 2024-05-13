import React, {Dispatch, SetStateAction} from 'react';
import {TFunction} from 'i18next';
import {User} from '@/entities/user/models/User.ts';

interface IProps {
    t: TFunction,
    name: string,
    lang: string,
    setParticipant: Dispatch<SetStateAction<User[]>>
}

const ProductTypeCombobox = () => {
    return (
        <div>

        </div>
    );
};

export default ProductTypeCombobox;