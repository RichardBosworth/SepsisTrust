﻿using System;

namespace SepsisTrust.Model.Azure
{
    public class AzureEntityData
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class ClinicalArea : AzureEntityData
    {
        public string Name { get; set; }
    }
}