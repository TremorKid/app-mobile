using System.Collections.Generic;
using System;

namespace Quiz.Model
{
    [Serializable]
    public class QuestionsTemplate
    {
        public List<Alternatives> questions;
    }
    
    [Serializable]
    public class Alternatives
    {
        public string question;
        
        public string alternative1;
        
        public string alternative2;
        
        public string alternative3;
        
        public string alternative4;
        
        public string alternative5;
        
        public string answer;
    }
}