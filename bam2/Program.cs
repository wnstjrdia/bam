using System;

namespace bam2
{
    class Program
    {
        static void Main(string[] args)
        {
            string str1, str2;//입력받고 학습할 단어2개            
            Console.Write("첫 번째 단어:");
            str1 = Console.ReadLine();
            Console.Write("두 번째 단어:");
            str2 = Console.ReadLine();

            string str3;//확인할 단어 입력
            Console.Write("확인할 단어:");
            str3 = Console.ReadLine();

            int[,] w = new int[(str1.Length * 7), (str2.Length * 7)];//가중치
            int[] st1 = new int[(str1.Length * 7)];//첫 단어 패턴
            int[] st2 = new int[(str2.Length * 7)];//두 번째 단어 패턴
            int[] st3 = new int[(str3.Length * 7)];//확인할 단어 패턴

            int total1 = 0, total2 = 0, total3 = 0;//실험할 단어를 어느쪽 패턴에 입력시킬지 정하기 위해 사용

            for (int i = 0; i < str1.Length; i++)//첫 단어 패턴 생성
            {
                string a = Convert.ToString(str1[i], 2);//2진수 값을 넣어줌
                total1 += Convert.ToInt32(str1[i]);
                for (int j = 0; j < a.Length; j++)
                {
                    if (a[j] == '0')//2진수에서 0값을 -1로 바꿔넣음
                    {
                        st1[(i * 7) + j] = -1;
                    }
                    else
                    {
                        st1[(i * 7) + j] = 1;
                    }
                }
            }

            for (int i = 0; i < str2.Length; i++)//두 번째 단어 패턴 생성
            {
                string a = Convert.ToString(str2[i], 2);//2진수 값을 넣어줌
                total2 += Convert.ToInt32(str2[i]);
                for (int j = 0; j < a.Length; j++)
                {
                    if (a[j] == '0')//2진수에서 0값을 -1로 바꿔넣음
                    {
                        st2[(i * 7) + j] = -1;
                    }
                    else
                    {
                        st2[(i * 7) + j] = 1;
                    }
                }
            }

            for (int i = 0; i < st1.Length; i++)//w 가중치값을 계산
            {
                for (int j = 0; j < st2.Length; j++)
                {
                    w[i, j] = st1[i] * st2[j];
                }
            }

            for (int i = 0; i < str3.Length; i++)//세 번째 단어 패턴 생성
            {
                string a = Convert.ToString(str3[i], 2);//2진수 값을 넣어줌
                total3 += Convert.ToInt32(str3[i]);
                for (int j = 0; j < a.Length; j++)
                {
                    if (a[j] == '0')//2진수에서 0값을 -1로 바꿔넣음
                    {
                        st3[(i * 7) + j] = -1;
                    }
                    else
                    {
                        st3[(i * 7) + j] = 1;
                    }
                }
            }

            total1 = Math.Abs(total3 - total1);
            total2 = Math.Abs(total3 - total2);

            int stlen = w.Length / st3.Length;//연상값의 글자개수를 알기위함
            int[] st4 = new int[stlen];
            string q = "";//단어의 아스키 코드값이 들어감
            int count = 1;//아스키 코드의 개수

            Console.Write("연상된 단어:");
            for (int i = 0; i < st4.Length; i++)//w값으로 연상값 구하기
            {
                for (int j = 0; j < st3.Length; j++)
                {
                    if (total1 > total2)
                    {
                        st4[i] += w[i, j] * st3[j];
                    }
                    else
                    {
                        st4[i] += w[j, i] * st3[j];
                    }
                }
                if (st4[i] < 0)
                {
                    st4[i] = 0;

                }
                else
                {
                    st4[i] = 1;
                }

                if (count == 7)
                {
                    q = q + Convert.ToString(st4[i]);
                    Console.Write((char)Convert.ToInt32(q, 2));
                    count = 1;
                    q = "";
                }
                else
                {
                    q = q + Convert.ToString(st4[i]);
                    count++;
                }
            }
            Console.WriteLine();

            count = 1;
            q = "";
            for (int i = 0; i < st4.Length; i++)
            {
                if (st4[i] == 0)
                {
                    st4[i] = -1;
                }
            }

            Console.Write("복구된 단어:");
            for (int i = 0; i < st3.Length; i++)//입력받았던 단어 복구
            {
                for (int j = 0; j < st4.Length; j++)
                {
                    if (total1 > total2)
                    {
                        st3[i] += w[j, i] * st4[j];
                    }
                    else
                    {
                        st3[i] += w[i, j] * st4[j];
                    }
                }
                if (st3[i] < 0)
                {
                    st3[i] = 0;
                }
                else
                {
                    st3[i] = 1;
                }

                if (count == 7)
                {
                    q = q + Convert.ToString(st3[i]);
                    Console.Write((char)Convert.ToInt32(q, 2));
                    count = 1;
                    q = "";
                }
                else
                {
                    q = q + Convert.ToString(st3[i]);
                    count++;
                }
            }
        }
    }
}
