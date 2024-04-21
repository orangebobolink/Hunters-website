import React, {useEffect, useState} from 'react';
import ManagingForm from '@/widgets/forms/ui/managing-form';
import {UserService} from '@/entities/user/UserService.ts';
import {User} from '@/entities/user/user.ts';
import AddUserDialog from '@/features/form/add-user-dialog.tsx';
import {Button} from '@/shared/ui';

const ManagingPage = () => {
    const [users, setUsers] = useState<User[]>([])
    const [isOpen, setIsOpen] = useState(false);
    const [count, setCount] = useState(0);

    useEffect(() => {
        const fetchUsers = async () => {
            try {
                const response = await UserService.getAll();
                setUsers(response.data);
                console.log(response.data)
            } catch (error) {
                console.error('Error fetching users:', error);
            }
        };

        fetchUsers();
    }, [count]);

    return (
        <div className="h-screen flex items-center flex-col">
            <AddUserDialog isOpen={isOpen} setIsOpen={setIsOpen} increaseCount={() => setCount(count + 1)}/>
            <Button onClick={()=>setIsOpen(true)}>Создать юзера</Button>
            <ManagingForm users={users} increaseCount={()=>setCount(count + 1)}/>
        </div>
    );
};

export default ManagingPage;