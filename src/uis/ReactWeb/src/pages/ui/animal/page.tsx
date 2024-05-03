import AnimalForm from "@/widgets/forms/ui/animal-form";
import {useEffect, useState} from "react";
import {Animal} from "@/entities/animal/Animal.ts";
import {AnimalService} from "@/entities/animal/AnimalService.ts";
import {Button} from '@/shared/ui';
import AddAnimalDialog from '@/features/dialog/add-animal-dialog.tsx';

const AnimalPage = () => {
    const [animals, setAnimals] = useState<Animal[]>([])
    const [isOpen, setIsOpen] = useState(false);

    useEffect(() => {
        const fetchUsers = async () => {
            try {
                const response = await AnimalService.getAll();
                setAnimals(response.data);
                console.log(response.data)
            } catch (error) {
                console.error('Error fetching users:', error);
            }
        };

        fetchUsers();
    }, []);

    return (
        <div className="select-none h-full w-full flex items-center flex-col justify-center">
            <AnimalForm animals={animals} handleClick={(animal:Animal) => {console.log(animal)}}/>
            <Button onClick={()=>{setIsOpen(true)}}>Добавить животное</Button>
            <AddAnimalDialog isOpen={isOpen} setIsOpen={setIsOpen}/>
        </div>
    );
};

export default AnimalPage;