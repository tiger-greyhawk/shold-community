using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Shold_Community
{
    
    public class ResponseYandexFotki
    {
        
        public string edited { get; set; }
        public string updated { get; set; }
        public Boolean xxx { get; set; }
        public class Img
        {
            public extraXXSmall XXXS { get; set; }
            public extraXSmall XXS  { get; set; }
            public extraSmall XS  { get; set; }
            public small S  { get; set; }
            public medium M  { get; set; }
            public large L  { get; set; }
            public extraLarge XL  { get; set; }
            public extraXLarge XXL  { get; set; }
            public origNew orig  { get; set; }
            public class extraXXSmall
            {
                public int width { get; set; }
                public string href { get; set; }
                public int height { get; set; }
            }
            public class extraXSmall
            {
                public int width { get; set; }
                public string href { get; set; }
                public int height { get; set; }
            }
            public class extraSmall
            {
                public int width { get; set; }
                public string href { get; set; }
                public int height { get; set; }
            }
            public class small
            {
                public int width { get; set; }
                public string href { get; set; }
                public int height { get; set; }
            }
            public class medium
            {
                public int width { get; set; }
                public string href { get; set; }
                public int height { get; set; }
            }
            public class large
            {
                public int width { get; set; }
                public string href { get; set; }
                public int height { get; set; }
            }
            public class extraLarge
            {
                public int width { get; set; }
                public string href { get; set; }
                public int height { get; set; }
            }
            public class extraXLarge
        {
                public int width { get; set; }
                public string href { get; set; }
                public int height { get; set; }
            }
            public class origNew
            {
                public int width { get; set; }
                public string href { get; set; }
                public int bytesize { get; set; }
                public int height { get; set; }
            }
        }
        public Img img { get; set; }
        public Tags tags { get; set; }
        public string title { get; set; }
        public string access { get; set; }
        public Boolean disableComments { get; set; }
        public class Authors
        {
            public string name { get; set; }
            public string uid { get; set; }
        }
        public Authors authors { get; set; }
        public Boolean hideOriginal { get; set; }
        public string author { get; set; }
        public string id { get; set; }
        public string published { get; set; }
       
        public class links
        {
            public string album { get; set; }
            public string editmedia { get; set; }
            public string self { get; set; }
            public string alternate { get; set; }
            public string edit { get; set; }
        }
        public class Tags { }

    }
}
/*
[DataContract]
public class ResponseYandexFotki
{
    [DataMember(Name = "edited")]
    public string Edited { get; set; }
    [DataMember(Name = "updated")]
    public string Updated { get; set; }
    [DataMember(Name = "xxx")]
    public Boolean Xxx { get; }
    [DataMember(Name = "img")]
    public Img Image { get; set; }
    [DataMember(Name = "tags")]
    public Tags TagsMy { get; set; }
    [DataMember(Name = "title")]
    public string Title { get; set; }
    [DataMember(Name = "access")]
    public string Access { get; set; }
    [DataMember(Name = "disableComments")]
    public Boolean DisableComments { get; set; }
    [DataMember(Name = "authors")]
    public Authors AuthorsMy { get; set; }
    [DataMember(Name = "hideOriginal")]
    public Boolean HideOriginal { get; set; }
    [DataMember(Name = "author")]
    public string Author { get; set; }
    [DataMember(Name = "id")]
    public string Id { get; set; }
    [DataMember(Name = "published")]
    public string Published { get; set; }
    [DataContract]
    public class Img
    {
        public XL ExtraLarge { get; set; }
        public class XXXS
        {
            public int width { get; set; }
            public string href { get; set; }
            public int height { get; set; }
        }
        public class XXS
        {
            public int width { get; set; }
            public string href { get; set; }
            public int height { get; set; }
        }
        public class XS
        {
            public int width { get; set; }
            public string href { get; set; }
            public int height { get; set; }
        }
        public class S
        {
            public int width { get; set; }
            public string href { get; set; }
            public int height { get; set; }
        }
        public class M
        {
            public int width { get; set; }
            public string href { get; set; }
            public int height { get; set; }
        }
        public class L
        {
            public int width { get; set; }
            public string href { get; set; }
            public int height { get; set; }
        }
        [DataContract]
        public class XL
        {
            public int width { get; set; }
            public string href { get; set; }
            public int height { get; set; }
        }
        public class XXL
        {
            public int width { get; set; }
            public string href { get; set; }
            public int height { get; set; }
        }
        public class orig
        {
            public int width { get; set; }
            public string href { get; set; }
            public int bytesize { get; set; }
            public int height { get; set; }
        }
    }
    public class links
    {
        public string album { get; set; }
        public string editmedia { get; set; }
        public string self { get; set; }
        public string alternate { get; set; }
        public string edit { get; set; }
    }
    public class Tags { }
    public class Authors
    {
        public string name { get; set; }
        public string uid { get; set; }
    }
}*/