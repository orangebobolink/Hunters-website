import { UserService } from '@/entities/user/api/UserService';
import { User } from '@/entities/user/models/User';
import { useAppSelector } from '@/shared/lib/hooks/redux-hooks';
import { selectAuth } from '@/shared/model/store/selectors/auth.selectors';
import { Avatar, AvatarFallback, AvatarImage } from '@/shared/ui/avatar';
import { Badge } from '@/shared/ui/badge';

import { useEffect, useState } from 'react';

const ProfileInformationPage = () => {
    const [user, setUser] = useState<User>();

    const { id } = useAppSelector(selectAuth);

    useEffect(() => {
        const fetchPermissions = async () => {
            try {
                const response = await UserService.getById(id!);
                setUser(response.data);
            } catch (error) {
                console.error('Error fetching users:', error);
            }
        };

        fetchPermissions();
    }, [id]);

    if (!user) {
        return <div>Loading...</div>;
    }

    const fullName = `${user.firstName} ${user.middleName || ''} ${
        user.lastName
    }`;
    const formattedDateOfBirth = new Date(
        user.dateOfBirth
    ).toLocaleDateString();

    return (
        <div className='p-6 bg-white  '>
            <div className='flex items-center justify-center mb-6'>
                <Avatar>
                    <AvatarImage src={user.avatarUrl} alt='@shadcn' />
                    <AvatarFallback>CN</AvatarFallback>
                </Avatar>
            </div>
            <div className='text-center'>
                <h1 className='text-2xl font-semibold text-gray-800'>
                    {fullName}
                </h1>
                <p className='text-gray-600'>@{user.userName}</p>
                <div className='flex justify-center mt-4 space-x-2'>
                    {user.roleNames.map((role) => (
                        <Badge key={role} variant='outline'>
                            {role}
                        </Badge>
                    ))}
                </div>
            </div>
            <div className='mt-6'>
                <p className='text-gray-600'>
                    <span className='font-semibold text-gray-800'>
                        Электронная почта:
                    </span>{' '}
                    {user.email}
                </p>
                <p className='text-gray-600'>
                    <span className='font-semibold text-gray-800'>
                        Номер телефона:
                    </span>{' '}
                    {user.phoneNumber}
                </p>
                <p className='text-gray-600'>
                    <span className='font-semibold text-gray-800'>
                        День рождения:
                    </span>{' '}
                    {formattedDateOfBirth}
                </p>
            </div>
        </div>
    );
};

export default ProfileInformationPage;
