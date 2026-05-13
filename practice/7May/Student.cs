namespace StudentClass.Students
{
   internal class Student
    {
        public int age {get; set;}
        public string name {get; set;}

        public Student(int _age, string _name)
        {
            age = _age;
            name = _name;
        }

        public void showDeatils()
        {
            Console.WriteLine(this.age);
            Console.WriteLine(this.name);
        }
    } 
}
