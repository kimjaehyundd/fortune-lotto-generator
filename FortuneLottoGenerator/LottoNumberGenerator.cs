using System;
using System.Collections.Generic;
using System.Linq;

namespace FortuneLottoGenerator
{
    public class LottoNumberGenerator
    {
        private Dictionary<string, int[]> elementNumbers;
        private Random random;

        public LottoNumberGenerator()
        {
            InitializeElementNumbers();
            random = new Random();
        }

        private void InitializeElementNumbers()
        {
            // 오행별 기본 숫자 범위
            elementNumbers = new Dictionary<string, int[]>();
            elementNumbers.Add("금", new int[] {1, 6, 11, 16, 21, 26, 31, 36, 41}); // 금속 성격의 숫자
            elementNumbers.Add("목", new int[] {3, 8, 13, 18, 23, 28, 33, 38, 43}); // 목속 성격의 숫자
            elementNumbers.Add("물", new int[] {2, 7, 12, 17, 22, 27, 32, 37, 42}); // 물속 성격의 숫자
            elementNumbers.Add("불", new int[] {4, 9, 14, 19, 24, 29, 34, 39, 44}); // 불속 성격의 숫자
            elementNumbers.Add("토", new int[] {5, 10, 15, 20, 25, 30, 35, 40, 45}); // 토속 성격의 숫자
        }

        public int[] GenerateNumbers(FortuneInterpretation interpretation)
        {
            var numbers = new HashSet<int>();
            var usedElements = new List<string>();

            // 주요 운세에서 2개 숫자 선택
            var mainElementNumbers = GetNumbersFromElement(interpretation.MainElement, 2);
            foreach (var num in mainElementNumbers)
            {
                numbers.Add(num);
            }
            usedElements.Add(interpretation.MainElement);

            // 강한 오행 요소들에서 추가 숫자 선택
            var strongElements = interpretation.ElementValues
                .Where(x => x.Value >= 2 && !usedElements.Contains(x.Key))
                .OrderByDescending(x => x.Value)
                .Take(3)
                .ToList();

            foreach (var element in strongElements)
            {
                var elementNumbers = GetNumbersFromElement(element.Key, 1);
                foreach (var num in elementNumbers)
                {
                    if (numbers.Count < 6)
                    {
                        numbers.Add(num);
                    }
                }
                usedElements.Add(element.Key);
            }

            // 부족한 숫자를 전체 범위에서 채우기
            while (numbers.Count < 6)
            {
                int randomNumber = random.Next(1, 46);
                numbers.Add(randomNumber);
            }

            // 수비학적 조정 (합이 120~180 사이가 되도록)
            var result = AdjustNumbersNumerologically(numbers.Take(6).ToArray());
            
            Array.Sort(result);
            return result;
        }

        private int[] GetNumbersFromElement(string element, int count)
        {
            if (!elementNumbers.ContainsKey(element))
                return new int[0];

            var availableNumbers = elementNumbers[element];
            var selectedNumbers = new List<int>();

            // 요소에 따른 숫자에서 랜덤 선택
            var shuffled = availableNumbers.OrderBy(x => random.Next()).ToArray();
            
            for (int i = 0; i < count && i < shuffled.Length; i++)
            {
                selectedNumbers.Add(shuffled[i]);
            }

            return selectedNumbers.ToArray();
        }

        private int[] AdjustNumbersNumerologically(int[] numbers)
        {
            int sum = numbers.Sum();
            var adjustedNumbers = new List<int>(numbers);

            // 이상적인 합 범위: 120-180
            if (sum < 120)
            {
                // 합이 너무 작으면 몇 개 숫자를 크게 조정
                for (int i = 0; i < adjustedNumbers.Count && sum < 120; i++)
                {
                    int increase = random.Next(1, 11);
                    if (adjustedNumbers[i] + increase <= 45)
                    {
                        sum = sum - adjustedNumbers[i] + (adjustedNumbers[i] + increase);
                        adjustedNumbers[i] += increase;
                    }
                }
            }
            else if (sum > 180)
            {
                // 합이 너무 크면 몇 개 숫자를 작게 조정
                for (int i = 0; i < adjustedNumbers.Count && sum > 180; i++)
                {
                    int decrease = random.Next(1, 11);
                    if (adjustedNumbers[i] - decrease >= 1)
                    {
                        sum = sum - adjustedNumbers[i] + (adjustedNumbers[i] - decrease);
                        adjustedNumbers[i] -= decrease;
                    }
                }
            }

            // 중복 제거
            var uniqueNumbers = new HashSet<int>(adjustedNumbers);
            while (uniqueNumbers.Count < 6)
            {
                int newNumber = random.Next(1, 46);
                uniqueNumbers.Add(newNumber);
            }

            return uniqueNumbers.Take(6).ToArray();
        }

        // 특별한 날짜나 상황에 따른 보너스 숫자
        public int[] GenerateSpecialNumbers(DateTime date)
        {
            var specialNumbers = new List<int>();
            
            // 날짜 기반 숫자
            specialNumbers.Add(date.Day % 45 + 1);
            specialNumbers.Add(date.Month * 3 % 45 + 1);
            specialNumbers.Add(date.Year % 45 + 1);
            
            // 요일 기반 숫자
            int dayOfWeek = (int)date.DayOfWeek;
            specialNumbers.Add((dayOfWeek * 7) % 45 + 1);
            
            return specialNumbers.Distinct().Take(4).ToArray();
        }
    }
}