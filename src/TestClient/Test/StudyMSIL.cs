using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace TestClient.Test {
    /// <summary>
    /// this code file is for study about MSIL.
    /// </summary>
    public class StudyMSIL {

        interface IAnimal {
            void Say (string a);
        }
        class Dog : IAnimal {

            //   IL_0001:  ldstr      "i am a dog:"
            //   IL_0006:  ldarg.1
            //   IL_0007:  call       string [System.Runtime]System.String::Concat(string,string)
            //   IL_000c:  call       void [System.Console]System.Console::WriteLine(string)
            public void Say (string a) {
                System.Console.WriteLine ("i am a dog:" + a);
            }

            public void Say3 (string a) {
                //   IL_0001:  ldarg.0
                //   IL_0002:  callvirt   instance string [System.Runtime]System.Object::ToString()
                //   IL_0007:  call       void [System.Console]System.Console::Write(string)
                //   IL_000c:  nop
                //   IL_000d:  ldarg.0
                //   IL_000e:  call       instance class [System.Runtime]System.Type [System.Runtime]System.Object::GetType()
                //   IL_0013:  callvirt   instance string [System.Runtime]System.Object::ToString()
                //   IL_0018:  call       void [System.Console]System.Console::Write(string)
                //   IL_001d:  nop
                //   IL_001e:  ldstr      "i am a dog:"
                //   IL_0023:  ldarg.1
                //   IL_0024:  call       string [System.Runtime]System.String::Concat(string,
                //                                                                     string)
                //   IL_0029:  call       void [System.Console]System.Console::WriteLine(string)
                System.Console.Write (this.ToString ());
                System.Console.Write (this.GetType ().FullName);
                System.Console.WriteLine ("i am a dog:" + a);
            }

            //   IL_0001:  ldstr      "i am a dog:"
            //   IL_0006:  ldarg.0
            //   IL_0007:  call       string [System.Runtime]System.String::Concat(string,string)
            //   IL_000c:  call       void [System.Console]System.Console::WriteLine(string)
            public static void Say2 (string a) {
                System.Console.WriteLine ("i am a dog:" + a);
            }

        }

        public void C<T> (int i, string s, object o) {

            Type t = typeof (T);

            int index = 123;
            System.Console.WriteLine (o.ToString ());

            List<object> fwe = new List<object> { i, s, o };

            string dd = (string) o;

            D (t, index, fwe);

        }

        public object D (Type t, int i, List<object> d) {

            return null;
        }

        public object E (Type t, int i, List<object> d) {
            List<object> fwe = new List<object> { };
            return null;
        }

        [NUnit.Framework.Test]
        public void Test () {
             Console.WriteLine("");
            Console.WriteLine (typeof (IAnimal).FullName);
            Console.WriteLine (typeof (IAnimal).AssemblyQualifiedName);
            Console.WriteLine (typeof (IAnimal).Name);
            Console.WriteLine (typeof (IAnimal).Namespace);

            Console.WriteLine ("FullName:" + this.GetType ().FullName);
            Console.WriteLine ("AssemblyQualifiedName:" + this.GetType ().AssemblyQualifiedName);
            Console.WriteLine ("Name:" + this.GetType ().Name);
            Console.WriteLine ("Namespace:" + this.GetType ().Namespace);
        }
    }
}