import {Table, TableBody, TableCell, TableHead, TableHeader, TableRow} from '@/shared/ui/table.tsx';
import {Button} from '@/shared/ui';
import {Animal} from '@/entities/animal/models/Animal.ts';
import {Avatar, AvatarFallback, AvatarImage} from '@/shared/ui/avatar.tsx';
import AnimalInfoDialog from '@/features/dialog/animalinfo-dialog.tsx';
import {useState} from 'react';

interface IProps{
    animals: Animal[],
    handleClick: (animal:Animal)=>void
}

const AnimalForm = ({animals, handleClick}:IProps) => {
    const [isOpen, setIsOpen] = useState(false)
    const [selectedAnimal, setSelectedAnimal] = useState<Animal>({} as Animal)

    return (
        <div className="w-1/2 flex justify-center">
            <Table className="justify-center">
                <TableHeader>
                    <TableRow>
                        <TableHead></TableHead>
                        <TableHead>Название</TableHead>
                        <TableHead>Тип</TableHead>
                        <TableHead></TableHead>
                    </TableRow>
                </TableHeader>
                <TableBody>
                    {animals.map((animal) => (

                          <TableRow key={animal.id}>
                            <TableCell>
                                <Avatar className="size-[2rem]">
                                    <AvatarImage src={animal.imageUrl} />
                                    <AvatarFallback>A</AvatarFallback>
                                </Avatar>
                            </TableCell>
                            <TableCell>{
                                animal.name
                             }</TableCell>
                            <TableCell>{animal.type}</TableCell>
                            <TableCell>
                                <Button
                                    variant="ghost"
                                    onClick={() => {
                                        setSelectedAnimal(animal)
                                        setIsOpen(true)
                                    }}
                                >
                                    Подробнее
                                </Button>
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
                { Object.keys(selectedAnimal).length !== 0 &&
                    <AnimalInfoDialog animal={selectedAnimal} isOpen={isOpen} setIsOpen={setIsOpen}/>}
        </div>
    );
};

export default AnimalForm;