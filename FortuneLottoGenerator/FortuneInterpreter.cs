using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FortuneLottoGenerator
{
    public class FortuneInterpretation
    {
        public string Explanation { get; set; }
        public Dictionary<string, int> ElementValues { get; set; }
        public List<int> LuckyNumbers { get; set; }
        public string MainElement { get; set; }

        public FortuneInterpretation()
        {
            ElementValues = new Dictionary<string, int>();
            LuckyNumbers = new List<int>();
        }
    }

    public class FortuneInterpreter
    {
        private readonly Dictionary<string, int> elementMappings;
        private readonly Dictionary<string, string> emotionElements;
        private readonly Dictionary<string, string> foodElements;
        private readonly Dictionary<string, string> timeElements;
        private readonly Dictionary<string, string> actionElements;

        public FortuneInterpreter()
        {
            InitializeMappings();
        }

        private void InitializeMappings()
        {
            // 오행 속성과 숫자 매핑
            elementMappings = new Dictionary<string, int>
            {
                {"금", 1}, {"목", 3}, {"물", 6}, {"불", 2}, {"토", 5}
            };

            // 감정과 오행 매핑
            emotionElements = new Dictionary<string, string>
            {
                {"배고", "토"}, {"배가고파", "토"}, {"배가고픈", "토"},
                {"슬픈", "금"}, {"슬픔", "금"}, {"슬픔다", "금"},
                {"기쁘", "불"}, {"행복", "불"}, {"좋아", "불"},
                {"화나", "불"}, {"화난", "불"}, {"짜증", "불"},
                {"고민", "목"}, {"걱정", "목"}, {"스트레스", "목"},
                {"피곤", "물"}, {"지치", "물"}, {"느낋", "물"}
            };

            // 음식과 오행 매핑
            foodElements = new Dictionary<string, string>
            {
                {"라면", "물"}, {"국물", "물"}, {"물", "물"},
                {"고기", "불"}, {"소시지", "불"}, {"돼지", "불"}, {"치킨", "불"},
                {"야채", "목"}, {"샐러드", "목"}, {"상추", "목"},
                {"빵", "토"}, {"밥", "토"}, {"감자", "토"},
                {"우유", "금"}, {"달가", "금"}, {"아이스크림", "금"}
            };

            // 시간과 오행 매핑
            timeElements = new Dictionary<string, string>
            {
                {"아침", "목"}, {"새벽", "목"}, {"오전", "목"},
                {"점심", "불"}, {"낮", "불"}, {"오후", "불"},
                {"저녁", "토"}, {"저녁식사", "토"}, {"저녁때", "토"},
                {"밤", "물"}, {"야식", "물"}, {"늘은밤", "물"},
                {"어제", "금"}, {"어제밤", "금"}, {"어젠", "금"}
            };

            // 행동과 오행 매핑
            actionElements = new Dictionary<string, string>
            {
                {"먹었", "토"}, {"먹어", "토"}, {"식사", "토"},
                {"잘", "물"}, {"잠", "물"}, {"침대", "물"},
                {"샰", "금"}, {"사죨", "금"}, {"돈", "금"},
                {"일", "불"}, {"일해", "불"}, {"공부", "불"},
                {"산책", "목"}, {"운동", "목"}, {"어전", "목"}
            };
        }

        public FortuneInterpretation InterpretStory(string story)
        {
            var interpretation = new FortuneInterpretation();
            
            // 이야기에서 오행 요소 분석
            AnalyzeElements(story, interpretation);
            
            // 주요 운세 결정
            DetermineMainElement(interpretation);
            
            // 사주팔자 스타일 해석 생성
            GenerateExplanation(interpretation, story);
            
            return interpretation;
        }

        private void AnalyzeElements(string story, FortuneInterpretation interpretation)
        {
            // 오행 요소 초기화
            interpretation.ElementValues["금"] = 0;
            interpretation.ElementValues["목"] = 0;
            interpretation.ElementValues["물"] = 0;
            interpretation.ElementValues["불"] = 0;
            interpretation.ElementValues["토"] = 0;

            // 감정 분석
            foreach (var emotion in emotionElements)
            {
                if (story.Contains(emotion.Key))
                {
                    interpretation.ElementValues[emotion.Value] += 2;
                }
            }

            // 음식 분석
            foreach (var food in foodElements)
            {
                if (story.Contains(food.Key))
                {
                    interpretation.ElementValues[food.Value] += 3;
                }
            }

            // 시간 분석
            foreach (var time in timeElements)
            {
                if (story.Contains(time.Key))
                {
                    interpretation.ElementValues[time.Value] += 1;
                }
            }

            // 행동 분석
            foreach (var action in actionElements)
            {
                if (story.Contains(action.Key))
                {
                    interpretation.ElementValues[action.Value] += 2;
                }
            }

            // 글자 수에 따른 보정
            int textLength = story.Length;
            string lengthElement = GetElementByNumber(textLength % 5);
            interpretation.ElementValues[lengthElement] += 1;
        }

        private void DetermineMainElement(FortuneInterpretation interpretation)
        {
            interpretation.MainElement = interpretation.ElementValues
                .OrderByDescending(x => x.Value)
                .First().Key;
        }

        private void GenerateExplanation(FortuneInterpretation interpretation, string story)
        {
            var explanation = new System.Text.StringBuilder();
            
            explanation.AppendLine("╭─────── 사주팔자 해석 ───────╮");
            explanation.AppendLine();
            
            // 주요 운세 해석
            explanation.AppendLine($"★ 주요 운세: {interpretation.MainElement}속({GetElementDescription(interpretation.MainElement)})");
            explanation.AppendLine();
            
            // 오행 분석 결과
            explanation.AppendLine("★ 오행 기운 분석:");
            foreach (var element in interpretation.ElementValues.OrderByDescending(x => x.Value))
            {
                if (element.Value > 0)
                {
                    explanation.AppendLine($"   {element.Key}속: {element.Value}점 - {GetElementMeaning(element.Key, element.Value)}");
                }
            }
            explanation.AppendLine();
            
            // 운세 해석
            explanation.AppendLine("★ 운세 해석:");
            explanation.AppendLine($"   {GetFortuneInterpretation(interpretation.MainElement, story)}");
            explanation.AppendLine();
            
            // 로또 번호 의미
            explanation.AppendLine("★ 로또 번호 의미:");
            explanation.AppendLine($"   주요 {interpretation.MainElement}속 기운이 강하여, 이를 바탕으로 한 행운의 숫자를 제시합니다.");
            
            explanation.AppendLine("╰───────────────────────────────────╯");
            
            interpretation.Explanation = explanation.ToString();
        }

        private string GetElementByNumber(int number)
        {
            string[] elements = { "금", "목", "물", "불", "토" };
            return elements[number % 5];
        }

        private string GetElementDescription(string element)
        {
            var descriptions = new Dictionary<string, string>
            {
                {"금", "의리와 청렴"}, {"목", "성장과 발전"}, {"물", "지혜와 유연성"},
                {"불", "열정과 활력"}, {"토", "안정과 신뢰"}
            };
            return descriptions.GetValueOrDefault(element, "비밀의 운세");
        }

        private string GetElementMeaning(string element, int value)
        {
            if (value >= 5) return "매우 강한 기운";
            if (value >= 3) return "강한 기운";
            if (value >= 1) return "약한 기운";
            return "없음";
        }

        private string GetFortuneInterpretation(string mainElement, string story)
        {
            var interpretations = new Dictionary<string, string[]>
            {
                {"금", new string[] {
                    "금속의 기운이 강합니다. 오늘은 의리와 정직이 행운을 가져다주는 날입니다.",
                    "금전운이 좋아 예상치 못한 수입이 있을 수 있습니다.",
                    "날카로운 판단력으로 중요한 결정을 내리기에 좋은 시기입니다."
                }},
                {"목", new string[] {
                    "목속의 성장 에너지가 강합니다. 새로운 시작과 발전에 좋은 날입니다.",
                    "창의적인 아이디어가 떠오르고 새로운 기회가 다가옵니다.",
                    "인간관계나 학습에서 좋은 성과를 거둘 수 있습니다."
                }},
                {"물", new string[] {
                    "물속의 지혜와 유연성이 돋보입니다. 직관이 매우 예리한 날입니다.",
                    "어려운 문제도 유연한 사고로 해결할 수 있을 것입니다.",
                    "휴식과 명상을 통해 내면의 힘을 찾을 수 있는 시기입니다."
                }},
                {"불", new string[] {
                    "불속의 열정과 활력이 넘칩니다. 적극적인 행동이 좋은 결과를 가져다줄 것입니다.",
                    "리더십을 발휘하여 주변 사람들에게 좋은 영향을 줄 수 있습니다.",
                    "경쟁이나 도전에서 승리할 가능성이 높습니다."
                }},
                {"토", new string[] {
                    "토속의 안정과 신뢰가 두드러집니다. 착실한 노력이 보답받는 날입니다.",
                    "가족이나 가까운 사람들과의 인연이 깊어질 것입니다.",
                    "장기적인 투자나 계획을 세우기에 좋은 시기입니다."
                }}
            };

            var elementInterpretations = interpretations.GetValueOrDefault(mainElement, 
                new string[] { "비밀스러운 운세가 당신을 기다리고 있습니다." });
            
            Random random = new Random(story.GetHashCode());
            return elementInterpretations[random.Next(elementInterpretations.Length)];
        }
    }
}