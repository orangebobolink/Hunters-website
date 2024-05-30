import React, {Dispatch, SetStateAction, useState} from 'react';
import {Type} from '@/entities/rent/models/Type.ts';

interface IProps {
    setProductTypes: Dispatch<SetStateAction<Type[]>>
}

function getEnumValues<T>(enumObj: T): T[keyof T][] {
    return Object.keys(enumObj)
        .filter(key => isNaN(Number(key)))
        .map(key => enumObj[key]);
}

const MultyProductTypeCombobox = ({
                                      setProductTypes
                                  }:IProps) => {
    const [value, setValue] = useState<Type[]>()

    return (
        <div></div>
    )
};

export default MultyProductTypeCombobox;